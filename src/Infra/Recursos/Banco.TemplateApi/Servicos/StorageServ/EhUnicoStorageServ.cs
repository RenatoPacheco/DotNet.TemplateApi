using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Recurso;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.StorageServ
{
    internal class EhUnicoStorageServ
        : BaseSimplesServico
    {
        public EhUnicoStorageServ(
            Conexao conexao)
            : base(conexao) { }

        public bool Executar(Storage dados)
        {
            ValidationNotification notificacoes = new ValidationNotification();

            ReferenciaEhUnico(dados);
            notificacoes.Add(this);

            Notifications.Clear();
            Notifications.Add(notificacoes);
            return IsValid();
        }

        private bool ReferenciaEhUnico(Storage dados)
        {
            Notifications.Clear();
            Mapeamentos.StorageMap map = new Mapeamentos.StorageMap();

            if (!(dados?.Referencia is null))
            {
                string sqlString = @$"
                    SELECT TOP 1 1
                    FROM {map.Tabela}
                    Where {map.Col(x => x.Referencia)} = @{map.Alias(x => x.Referencia)}
                    AND {map.Col(x => x.Id)} <> @{map.Alias(x => x.Id)}
                    AND {map.Col(x => x.Status)} <> @{map.Alias(x => x.Status)}
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Id)}", dados.Id ?? 0 },
                    { $"{map.Alias(x => x.Referencia)}", dados.Referencia },
                    { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(Status.Excluido) }
                };

                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(
                    sqlString, sqlParam, Conexao.Transicao);

                if (resultado.Any())
                {
                    Notifications.AddError<Storage>(
                        x => x.Referencia, AvisosResx.XNaoEhUnico, null);
                }
            }

            return Notifications.IsValid();
        }
    }
}
