using Dapper;
using System.Linq;
using System.Text;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Compartilhado.Json;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class FiltrarStoragePers : Comum.BuscaRepositorio
    {
        public FiltrarStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        private StorageMap _map;

        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando, string referencia)
        {
            return Filtrar(comando, referencia);
        }

        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando, string referencia = "", ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();
            StringBuilder sql = new StringBuilder();
            StringBuilder sqlFiltro = new StringBuilder();
            StringBuilder sqlTextos = new StringBuilder();
            IDictionary<string, object> sqlObjeto = new Dictionary<string, object>();
            IList<string> textos = DesmebrarTexto(comando.Texto);

            _map = new StorageMap { RefSql = "sto" };

            bool haPaginacao = HaPaginacao(comando);

            sql.Append($" FROM [{_map.Tabela}] as sto ");

            if (comando.Storage.Any())
            {
                sqlFiltro.Append($" AND sto.[{_map.Col(x => x.Id)}] IN @Storage ");
                sqlObjeto.Add("Storage", comando.Storage);
            }

            if (comando.Referencia.Any())
            {
                sqlFiltro.Append($" AND sto.[{_map.Col(x => x.Referencia)}] IN @Referencia ");
                sqlObjeto.Add("Referencia", comando.Referencia);
            }

            if (comando.Alias.Any())
            {
                sqlFiltro.Append($" AND sto.[{_map.Col(x => x.Alias)}] IN @Alias ");
                sqlObjeto.Add("Alias", comando.Alias);
            }

            if (comando.Status.Any())
            {
                sqlFiltro.Append($" AND sto.[{_map.Col(x => x.Status)}] IN @Status ");
                sqlObjeto.Add("Status", StatusAdapt.EnumParaSql(comando.Status));
            }

            if (textos.Any())
            {
                sqlFiltro.Append(" AND ( ");
                for (int i = 0; i < textos.Count; i++)
                {
                    sqlTextos.Append($" OR {_map.Col(x => x.Nome)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Referencia)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");
                    sqlTextos.Append($" OR {_map.Col(x => x.Tipo)} collate SQL_Latin1_general_CP1_CI_AI LIKE @Texto{i} ");

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
            sqlConsulta.Append($" ORDER BY sto.[{_map.Col(x => x.Id)}] DESC ");
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

                resultado.ResultadosDaPaginaAtual = ContratoJson.Desserializar<Storage[]>(
                    json.Any() ? string.Join("", json) : "[]");
            }

            if (!resultado.ResultadosDaPaginaAtual.Any())
            {
                if (comando.Maximo != 1)
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrados, NomesResx.Storages), referencia, tipo));
                }
                else
                {
                    Notifications.Add(new ValidationMessage(
                        string.Format(AvisosResx.XNaoEncontrado, NomesResx.Storage), referencia, tipo));
                }
            }

            return resultado;
        }
    }
}
