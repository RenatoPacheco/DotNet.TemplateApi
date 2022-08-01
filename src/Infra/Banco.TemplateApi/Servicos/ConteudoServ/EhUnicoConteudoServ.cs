using Dapper;
using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.RecursoResx;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Banco.TemplateApi.Servicos.ConteudoServ
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
                IEnumerable<dynamic> resultado = Conexao.Sessao.Query(@$"
                    SELECT TOP 1 1
                    FROM {map.Tabela}
                    Where {map.Col(x => x.Alias)} = @Alias
                    AND {map.Col(x => x.Id)} <> @Id
                    AND {map.Col(x => x.Status)} <> @Status
                ", new
                {
                    Id = dados.Id ?? 0,
                    Alias = dados.Alias,
                    Status = StatusAdapt.EnumParaSql(Status.Excluido)
                }, Conexao.Transicao);

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
