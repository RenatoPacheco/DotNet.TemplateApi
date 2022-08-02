using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.StorageServ
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
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM {map.Tabela}
                    Where {map.Col(x => x.Referencia)} = @Referencia
                    AND {map.Col(x => x.Id)} <> @Id
                    AND {map.Col(x => x.Status)} <> @Status
                ", new
                {
                    Id = dados.Id ?? 0,
                    Referencia = dados.Referencia,
                    Status = StatusAdapt.EnumParaSql(Status.Excluido)
                }, Conexao.Transicao);

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
