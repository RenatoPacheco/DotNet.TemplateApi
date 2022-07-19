using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using DotNetCore.API.Template.RecursoResx;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.MapeamentoSql;

namespace DotNetCore.API.Template.Repositorio.Persistencias.UsuarioPers
{
    class EhUnicoUsuarioPers : Comum.SimplesRep
    {
        public EhUnicoUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public bool EhUnico(Usuario dados)
        {
            ValidationNotification notificacoes = new ValidationNotification();

            EmailEhUnico(dados);
            notificacoes.Add(this);

            Notifications.Clear();
            Notifications.Add(notificacoes);
            return IsValid();
        }

        private bool EmailEhUnico(Usuario dados)
        {
            Notifications.Clear();
            UsuarioMapSql json = new UsuarioMapSql();

            if (!(dados?.Email is null))
            {
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM [{json.Tabela}]
                    Where [{json.Coluna(x => x.Email)}] = @Email
                    AND [{json.Coluna(x => x.Id)}] <> @Id
                    AND [{json.Coluna(x => x.Status)}] <> @Status
                ", new
                {
                    Id = dados.Id ?? 0,
                    Email = new DbString { Value = dados.Email, IsAnsi = true },
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
