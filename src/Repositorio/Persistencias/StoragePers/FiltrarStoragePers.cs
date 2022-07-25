using Dapper;
using System.Linq;
using System.Text;
using TemplateApi.RecursoResx;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Dominio.Interfaces;
using System.Text.RegularExpressions;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Dominio.Comandos.StorageCmds;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class FiltrarStoragePers : Comum.BuscaRepositorio
    {
        public FiltrarStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        private StorageMap _map;

        public ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, string referencia)
        {
            return Filtrar(comando, referencia);
        }

        public ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();
            StringBuilder sql = new StringBuilder();
            IDictionary<string, object> sqlParametros = new Dictionary<string, object>();
            bool haPaginacao = HaPaginacao(comando);

            _map = new StorageMap { RefSql = "sto" };

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

                resultado.ResultadosDaPaginaAtual = jsonResult.ParseJson<Storage[]>();
            }

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                string mensagem = string.Format(AvisosResx.XNaoEncontrados, NomesResx.Storages);

                if (comando.Maximo == 1)
                {
                    mensagem = string.Format(AvisosResx.XNaoEncontrado, NomesResx.Storage);
                }

                Notifications.Add(new ValidationMessage(mensagem, referencia, tipo));
            }

            return resultado;
        }

        private void AplicarFiltro(FiltrarStorageCmd comando,
            ref StringBuilder sql, ref IDictionary<string, object> sqlParametros)
        {
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlTextos = new StringBuilder();
            IList<string> textos = DesmebrarTexto(comando.Texto);

            sql.Append($" FROM {_map.Tabela} ");

            if (comando.Storage.Any())
            {
                sqlFiltro.Append($" AND {_map.Col(x => x.Id)} IN @Storage ");
                sqlParametros.Add("Storage", comando.Storage);
            }

            if (comando.Referencia.Any())
            {
                sqlFiltro.Append($" AND {_map.Col(x => x.Referencia)} IN @Referencia ");
                sqlParametros.Add("Referencia", comando.Referencia);
            }

            if (comando.Alias.Any())
            {
                sqlFiltro.Append($" AND {_map.Col(x => x.Alias)} IN @Alias ");
                sqlParametros.Add("Alias", comando.Alias);
            }

            if (comando.Status.Any())
            {
                sqlFiltro.Append($" AND sto.{_map.Col(x => x.Status)} IN @Status ");
                sqlParametros.Add("Status", StatusAdapt.EnumParaSql(comando.Status));
            }

            if (textos.Any())
            {
                sqlFiltro.Append(" AND ( ");
                for (int i = 0; i < textos.Count; i++)
                {
                    sqlTextos.Append($" OR {_map.Col(x => x.Nome)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Referencia)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Tipo)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");

                    sqlParametros.Add($"Texto{i}", $"%{textos[i]}%");
                }
                sqlFiltro.Append(Regex.Replace(sqlTextos.ToString(), @"^\s+OR\s+", ""));
                sqlFiltro.Append(" ) ");
                sqlTextos.Clear();
            }

            sql.Append(Regex.Replace(sqlFiltro.ToString(), @"^\s+AND\s+", " WHERE "));
        }

        private void AplicarPaginacao(
            ref ResultadoBusca<Storage> resultado,
            IDictionary<string, object> sqlParametros,
            FiltrarStorageCmd comando, StringBuilder sql)
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
