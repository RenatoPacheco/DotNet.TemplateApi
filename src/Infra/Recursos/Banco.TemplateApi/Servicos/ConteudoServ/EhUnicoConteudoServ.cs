using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Recurso;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class EhUnicoConteudoServ
        : BaseSimplesServico
    {
        public EhUnicoConteudoServ(
            Conexao conexao)
            : base(conexao) { }

        public bool Executar(Conteudo dados)
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
            Mapeamentos.ConteudoMap map = new Mapeamentos.ConteudoMap();

            if (!(dados?.Alias is null))
            {
                string sqlString = @$"
                    SELECT TOP 1 1
                    FROM {map.Tabela}
                    Where {map.Col(x => x.Alias)} = @{map.Alias(x => x.Alias)}
                    AND {map.Col(x => x.Id)} <> @{map.Alias(x => x.Id)}
                    AND {map.Col(x => x.Status)} <> @{map.Alias(x => x.Status)}
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Id)}", dados.Id ?? 0 },
                    { $"{map.Alias(x => x.Alias)}", dados.Alias },
                    { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(Status.Excluido) }
                };

                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(
                    sqlString, sqlParam, Conexao.Transicao);

                if (resultado.Any())
                {
                    Notifications.AddError<Conteudo>(
                        x => x.Alias, AvisosResx.XNaoEhUnico, null);
                }
            }

            return Notifications.IsValid();
        }
    }
}
