using Dapper;
using System.Linq;
using System.Text;
using BitHelp.Core.Validation;
using TemplateApi.RecursoResx;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class FiltrarUsuarioPers : Comum.BuscaRepositorio
    {
        public FiltrarUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        private UsuarioMap _map;

        public ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, string referencia)
        {
            return Filtrar(comando, referencia);
        }

        public ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Usuario> resultado = new ResultadoBusca<Usuario>();
            StringBuilder sql = new StringBuilder();
            IDictionary<string, object> sqlParametros = new Dictionary<string, object>();
            bool haPaginacao = HaPaginacao(comando);

            _map = new UsuarioMap { RefSql = "usu" };

            AplicarFiltro(comando, ref sql, ref sqlParametros);

            if (haPaginacao)
            {
                AplicarPaginacao(ref resultado, 
                    sqlParametros, comando, sql);
            }

            if (resultado.TotalDePaginas >= comando.Pagina || !haPaginacao)
            {
                StringBuilder sqlConsulta = new StringBuilder();
                sqlConsulta.Append($" SELECT {_map}");
                sqlConsulta.Append(sql);
                sqlConsulta.Append($" ORDER BY {_map.Col(x => x.Id)} DESC ");
                sqlConsulta.Append(MontarPaginacao(comando));
                sqlConsulta.Append(" FOR JSON PATH ");

                IEnumerable<string> jsonList = Conexao.Sessao.Query<string>(
                   sqlConsulta.ToString(), sqlParametros, Conexao.Transicao);

                string jsonResult = (jsonList.Any() ? string.Join("", jsonList) : "[]");

                resultado.ResultadosDaPaginaAtual = jsonResult.ParseJson<Usuario[]>();
            }

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                string mensagem = string.Format(AvisosResx.XNaoEncontrados, NomesResx.Usuarios);

                if (comando.Maximo == 1)
                {
                    mensagem = string.Format(AvisosResx.XNaoEncontrado, NomesResx.Usuario);
                }

                Notifications.Add(new ValidationMessage(mensagem, referencia, tipo));
            }

            return resultado;
        }

        private void AplicarFiltro(FiltrarUsuarioCmd comando,
            ref StringBuilder sql, ref IDictionary<string, object> sqlParametros)
        {
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlTextos = new StringBuilder();
            IList<string> textos = DesmebrarTexto(comando.Texto);

            sql.Append($" FROM {_map.Tabela} ");

            if (comando.Usuario.Any())
            {
                sqlFiltro.Append($" AND {_map.Col(x => x.Id)} IN @Usuario ");
                sqlParametros.Add("Usuario", comando.Usuario);
            }

            if (comando.Status.Any())
            {
                sqlFiltro.Append($" AND {_map.Col(x => x.Status)} IN @Status ");
                sqlParametros.Add("Status", StatusAdapt.EnumParaSql(comando.Status));
            }

            if (textos.Any())
            {
                sqlFiltro.Append(" AND ( ");
                for (int i = 0; i < textos.Count; i++)
                {
                    sqlTextos.Append($" OR {_map.Col(x => x.Nome)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Email)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");

                    sqlParametros.Add($"Texto{i}", $"%{textos[i]}%");
                }
                sqlFiltro.Append(Regex.Replace(sqlTextos.ToString(), @"^\s+OR\s+", ""));
                sqlFiltro.Append(" ) ");
                sqlTextos.Clear();
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^\s+AND\s+", " WHERE "));
        }

        private void AplicarPaginacao(
            ref ResultadoBusca<Usuario> resultado,
            IDictionary<string, object> sqlParametros,
            FiltrarUsuarioCmd comando, StringBuilder sql)
        {
            StringBuilder sqlContagem = new StringBuilder();
            sqlContagem.Append($" SELECT count(*) ");
            sqlContagem.Append(sql);

            int total = Conexao.Sessao.QuerySingleOrDefault<int>(
                sqlContagem.ToString(), sqlParametros, Conexao.Transicao);

            resultado.CalcularPaginas(total, comando.Maximo);
        }
    }
}
