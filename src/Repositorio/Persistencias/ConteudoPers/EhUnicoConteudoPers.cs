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

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    class EhUnicoConteudoPers : Comum.SimplesRepositorio
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
            ConteudoMap map = new ConteudoMap();

            if (!(dados?.Alias is null))
            {
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM [{map.Tabela}]
                    Where [{map.Col(x => x.Alias)}] = @Alias
                    AND [{map.Col(x => x.Id)}] <> @Id
                    AND [{map.Col(x => x.Status)}] <> @Status
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
