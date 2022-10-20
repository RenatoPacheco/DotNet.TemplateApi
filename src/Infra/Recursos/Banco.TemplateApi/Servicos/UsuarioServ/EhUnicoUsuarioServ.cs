using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.UsuarioServ
{
    internal class EhUnicoUsuarioServ
        : BaseSimplesServico
    {
        public EhUnicoUsuarioServ(
            Conexao conexao)
            : base(conexao) { }

        public bool Executar(Usuario dados)
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
            Mapeamentos.UsuarioMap map = new Mapeamentos.UsuarioMap();

            if (!(dados?.Email is null))
            {
                string sqlString = @$"
                    SELECT TOP 1 1
                    FROM {map.Tabela}
                    Where {map.Col(x => x.Email)} = @{map.Alias(x => x.Email)}
                    AND {map.Col(x => x.Id)} <> @{map.Alias(x => x.Id)}
                    AND {map.Col(x => x.Status)} <> @{map.Alias(x => x.Status)}
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Id)}", dados.Id ?? 0 },
                    { $"{map.Alias(x => x.Email)}", dados.Email },
                    { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(Status.Excluido) }
                };

                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(
                    sqlString, sqlParam, Conexao.Transicao);

                if (resultado.Any())
                {
                    Notifications.AddError<Usuario>(
                        x => x.Email, AvisosResx.XNaoEhUnico, null);
                }
            }

            return Notifications.IsValid();
        }
    }
}
