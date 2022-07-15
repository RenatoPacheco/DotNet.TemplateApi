using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using DotNetCore.API.Template.Recurso;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.MapeamentoSql;

namespace DotNetCore.API.Template.Repositorio.Persistencias.ConteudoPers
{
    class EhUnicoConteudoPers : Comum.SimplesRep
    {
        public EhUnicoConteudoPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public bool EhUnico(Conteudo dados)
        {
            ValidationNotification notificacoes = new ValidationNotification();

            AliasEhUnico(dados);
            notificacoes.Add(this);

            Notifications.Clear();
            Notifications.Add(notificacoes);
            return IsValid();
        }

        private bool AliasEhUnico(Conteudo dados)
        {
            Notifications.Clear();
            ConteudoMapSql json = new ConteudoMapSql();

            if (!(dados?.Alias is null))
            {
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM [{json.Tabela}]
                    Where [{json.Coluna(x => x.Alias)}] = @Alias
                    AND [{json.Coluna(x => x.Id)}] <> @Id
                    AND [{json.Coluna(x => x.Status)}] <> @Status
                ", new
                {
                    Id = dados.Id ?? 0,
                    Alias = new DbString { Value = dados.Alias, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(Status.Excluido), IsAnsi = true }
                }, Conexao.Transicao);

                if (resultado.Any())
                {
                    Notifications.AddError(
                        string.Format(AvisosResx.XNaoEhUnico, "Alias"));
                }
            }

            return Notifications.IsValid();
        }
    }
}
