using Dapper;
using System.Linq;
using System.Text;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Compartilhado.Json;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class FiltrarUsuarioPers : Comum.BuscaRepositorio
    {
        public FiltrarUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        private UsuarioMap _map;

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando, string referencia)
        {
            return Filtrar(comando, referencia);
        }

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando, string referencia = "", ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Usuario> resultado = new ResultadoBusca<Usuario>();
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlTextos = new StringBuilder();
            IDictionary<string, object> sqlObjeto = new Dictionary<string, object>();
            IList<string> textos = DesmebrarTexto(comando.Texto);

            _map = new UsuarioMap { RefSql = "usu" };

            bool haPaginacao = HaPaginacao(comando);

            sql.Append($" FROM [{_map.Tabela}] as usu ");

            if (comando.Usuario.Any())
            {
                sqlFiltro.Append($" AND usu.[{_map.Col(x => x.Id)}] IN @Usuario ");
                sqlObjeto.Add("Usuario", comando);
            }

            if (comando.Status.Any())
            {
                sqlFiltro.Append($" AND usu.[{_map.Col(x => x.Status)}] IN @Status ");
                sqlObjeto.Add("Status", StatusAdapt.EnumParaSql(comando.Status));
            }

            if (textos.Any())
            {
                sqlFiltro.Append(" AND ( ");
                for (int i = 0; i < textos.Count; i++)
                {
                    sqlTextos.Append($" OR {_map.Col(x => x.Nome)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Email)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");

                    sqlObjeto.Add($"Texto{i}", new DbString { Value = $"%{textos[i]}%", IsAnsi = true });
                }
                sqlFiltro.Append(Regex.Replace(sqlTextos.ToString(), @"^\s+OR\s+", ""));
                sqlFiltro.Append(" ) ");
                sqlTextos.Clear();
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^\s+AND\s+", " WHERE "));

            StringBuilder sqlConsulta = new StringBuilder();

            sqlConsulta.Append($" SELECT {_map}");
            sqlConsulta.Append(sql);
            sqlConsulta.Append($" ORDER BY usu.[{_map.Col(x => x.Id)}] DESC ");
            sqlConsulta.Append(MontarPaginacao(comando));
            sqlConsulta.Append(" FOR JSON PATH ");

            StringBuilder sqlContagem = new StringBuilder();
            sqlContagem.Append($" SELECT count(*) ");
            sqlContagem.Append(sql);

            if (haPaginacao)
            {
                int total = Conexao.Sessao.QuerySingleOrDefault<int>(
                    sqlContagem.ToString(), sqlObjeto, Conexao.Transicao);

                resultado.CalcularPaginas(total, comando.Maximo);
            }

            if (resultado.TotalDePaginas >= comando.Pagina || !haPaginacao)
            {
                IEnumerable<string> json = Conexao.Sessao.Query<string>(
                   sqlConsulta.ToString(), sqlObjeto, Conexao.Transicao);

                resultado.ResultadosDaPaginaAtual = ContratoJson.Desserializar<Usuario[]>(
                    json.Any() ? string.Join("", json) : "[]");
            }

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                if (comando.Maximo != 1)
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrados, NomesResx.Usuarios), referencia, tipo));
                }
                else
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrado, NomesResx.Usuario), referencia, tipo));
                }
            }

            return resultado;
        }
    }
}
