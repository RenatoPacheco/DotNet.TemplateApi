using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    class EhUnicoUsuarioPers : Comum.SimplesRepositorio
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
            UsuarioMap map = new UsuarioMap();

            if (!(dados?.Email is null))
            {
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM {map.Tabela}
                    Where {map.Col(x => x.Email)} = @Email
                    AND {map.Col(x => x.Id)} <> @Id
                    AND {map.Col(x => x.Status)} <> @Status
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
