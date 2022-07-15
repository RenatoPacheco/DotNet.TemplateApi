using Dapper;
using System.Linq;
using System.Text;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DotNetCore.API.Template.Recurso;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Compartilhado.Json;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Repositorio.MapeamentoSql;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds;

namespace DotNetCore.API.Template.Repositorio.Persistencias.ConteudoPers
{
    internal class FiltrarConteudoPers : Comum.BuscaResp
    {
        public FiltrarConteudoPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        private ConteudoMapSql _jsonConteudo;

        public ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando, string referencia)
        {
            return Filtrar(comando, referencia);
        }

        public ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando, string referencia = "", ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Conteudo> resultado = new ResultadoBusca<Conteudo>();
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlTextos = new StringBuilder();
            IDictionary<string, object> sqlObjeto = new Dictionary<string, object>();
            IList<string> textos = DesmebrarTexto(comando.Texto);

            _jsonConteudo = new ConteudoMapSql { RefSql = "usu" };

            bool haPaginacao = HaPaginacao(comando);

            sql.Append($" FROM [{_jsonConteudo.Tabela}] as usu ");

            if (comando.Conteudo.Any())
            {
                sqlFiltro.Append($" AND usu.[{_jsonConteudo.Coluna(x => x.Id)}] IN @Conteudo ");
                sqlObjeto.Add("Conteudo", comando);
            }

            if (comando.Status.Any())
            {
                sqlFiltro.Append($" AND usu.[{_jsonConteudo.Coluna(x => x.Status)}] IN @Status ");
                sqlObjeto.Add("Status", StatusAdapt.EnumParaSql(comando.Status));
            }

            if (textos.Any())
            {
                sqlFiltro.Append(" AND ( ");
                for (int i = 0; i < textos.Count; i++)
                {
                    sqlTextos.Append($" OR {_jsonConteudo.Coluna(x => x.Titulo)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_jsonConteudo.Coluna(x => x.Alias)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_jsonConteudo.Coluna(x => x.Texto)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");

                    sqlObjeto.Add($"Texto{i}", new DbString { Value = $"%{textos[i]}%", IsAnsi = true });
                }
                sqlFiltro.Append(Regex.Replace(sqlTextos.ToString(), @"^\s+OR\s+", ""));
                sqlFiltro.Append(" ) ");
                sqlTextos.Clear();
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^\s+AND\s+", " WHERE "));

            StringBuilder sqlConsulta = new StringBuilder();

            sqlConsulta.Append($" SELECT {_jsonConteudo}");
            sqlConsulta.Append(sql);
            sqlConsulta.Append($" ORDER BY usu.[{_jsonConteudo.Coluna(x => x.Id)}] DESC ");
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

                resultado.ResultadosDaPaginaAtual = ContratoJson.Desserializar<Conteudo[]>(
                    json.Any() ? string.Join("", json) : "[]");
            }

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                if (comando.Maximo != 1)
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrados, NomesResx.Conteudos), referencia, tipo));
                }
                else
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrado, NomesResx.Conteudo), referencia, tipo));
                }
            }

            return resultado;
        }
    }
}
