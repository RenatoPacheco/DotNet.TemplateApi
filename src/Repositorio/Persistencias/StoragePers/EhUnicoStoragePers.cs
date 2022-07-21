using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    class EhUnicoStoragePers : Comum.SimplesRepositorio
    {
        public EhUnicoStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public bool EhUnico(Storage dados)
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
            StorageMap map = new StorageMap();

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
                    Referencia = new DbString { Value = dados.Referencia, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(Status.Excluido), IsAnsi = true }
                }, Conexao.Transicao);

                if (resultado.Any())
                {
                    Notifications.AddError(
                        string.Format(AvisosResx.XNaoEhUnico, "E-mail"));
                }
            }

            return Notifications.IsValid();
        }
    }
}
