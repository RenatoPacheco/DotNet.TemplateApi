using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using DotNetCore.API.Template.Recurso;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.MapeamentoSql;

namespace DotNetCore.API.Template.Repositorio.Persistencias.StoragePers
{
    class EhUnicoStoragePers : Comum.SimplesRep
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
            StorageMapSql json = new StorageMapSql();

            if (!(dados?.Referencia is null))
            {
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM [{json.Tabela}]
                    Where [{json.Coluna(x => x.Referencia)}] = @Referencia
                    AND [{json.Coluna(x => x.Id)}] <> @Id
                    AND [{json.Coluna(x => x.Status)}] <> @Status
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
