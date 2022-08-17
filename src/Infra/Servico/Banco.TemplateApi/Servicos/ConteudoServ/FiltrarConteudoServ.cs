using Dapper;
using System.Linq;
using System.Text;
using TemplateApi.RecursoResx;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Dominio.Entidades;
using System.Text.RegularExpressions;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class FiltrarConteudoServ
        : BaseBuscaServico
    {
        public FiltrarConteudoServ(
            Conexao conexao)
            : base(conexao) { }

        private Mapeamentos.ConteudoMap _map;

        public ResultadoBusca<Conteudo> Executar(
            FiltrarConteudoCmd comando, string referencia)
        {
            return Executar(comando, referencia);
        }

        public ResultadoBusca<Conteudo> Executar(
            FiltrarConteudoCmd comando, ValidationType tipo)
        {
            return Executar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Conteudo> Executar(
            FiltrarConteudoCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Conteudo> resultado = new ResultadoBusca<Conteudo>();
            StringBuilder sql = new StringBuilder();
            IDictionary<string, object> sqlParametros = new Dictionary<string, object>();

            _map = new Mapeamentos.ConteudoMap { RefSql = "cont" };

            AplicarFiltro(comando, ref sql, ref sqlParametros);

            CalcularPaginacao(ref resultado, sqlParametros, comando, sql);

            if (resultado.TotalDePaginas >= comando.Pagina
                || (comando.Maximo > 0 && comando.Maximo < int.MaxValue))
            {
                if (comando.Contexto == ContextoCmd.Embutir)
                {
                    _map.Ignorar(x => x.Texto);
                }

                StringBuilder sqlConsulta = new StringBuilder();
                sqlConsulta.Append($" SELECT {_map}");
                sqlConsulta.Append(sql);
                sqlConsulta.Append($" ORDER BY {_map.Col(x => x.Id)} DESC ");
                sqlConsulta.Append(MontarPaginacao(comando));
                sqlConsulta.Append(" FOR JSON PATH ");

                IEnumerable<string> jsonList = Conexao.Sessao.Query<string>(
                   sqlConsulta.ToString(), sqlParametros, Conexao.Transicao);

                string jsonResult = (jsonList.Any() ? string.Join("", jsonList) : "[]");

                resultado.ResultadosDaPaginaAtual = jsonResult.ParseJson<Conteudo[]>();
            }

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                string mensagem = string.Format(AvisosResx.XNaoEncontrados, NomesResx.Conteudos);

                if (comando.Maximo == 1)
                {
                    mensagem = string.Format(AvisosResx.XNaoEncontrado, NomesResx.Conteudo);
                }

                Notifications.Add(new ValidationMessage(mensagem, referencia, tipo));
            }

            return resultado;
        }

        private void AplicarFiltro(FiltrarConteudoCmd comando,
            ref StringBuilder sql, ref IDictionary<string, object> sqlParametros)
        {
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlTextos = new StringBuilder();
            IList<string> textos = DesmebrarTexto(comando.Texto);

            sql.Append($" FROM {_map.Tabela} ");

            if (comando.Conteudo.Any())
            {
                sqlFiltro.Append($" AND {_map.Col(x => x.Id)} IN @Conteudo ");
                sqlParametros.Add("Conteudo", comando.Conteudo);
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
                    sqlTextos.Append($" OR {_map.Col(x => x.Titulo)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Alias)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Texto)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");

                    sqlParametros.Add($"Texto{i}", $"%{textos[i]}%");
                }
                sqlFiltro.Append(Regex.Replace(sqlTextos.ToString(), @"^\s+OR\s+", ""));
                sqlFiltro.Append(" ) ");
                sqlTextos.Clear();
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^\s+AND\s+", " WHERE "));
        }

        private void CalcularPaginacao(
            ref ResultadoBusca<Conteudo> resultado,
            IDictionary<string, object> sqlParametros,
            FiltrarConteudoCmd comando, StringBuilder sql)
        {
            if (comando.CalcularPaginacao)
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
}
