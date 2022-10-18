using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class ExcluirConteudoServ
        : BaseSimplesServico
    {
        public ExcluirConteudoServ(
            Conexao conexao)
            : base(conexao) { }

        public void Executar(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();
            Mapeamentos.ConteudoMap map = new Mapeamentos.ConteudoMap();

            string sqlString = @$"
                    UPDATE {map.Tabela} SET
                            {map.Col(x => x.AlteradoEm)} = @{map.Alias(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)} = @{map.Alias(x => x.Status)}
                    WHERE {map.Col(x => x.Id)} IN @{map.Alias(x => x.Id)}
                ";

            IDictionary<string, object> sqlParam = new Dictionary<string, object>
            {
                { $"{map.Alias(x => x.Id)}", comando.Conteudo },
                { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(Status.Excluido) },
                { $"{map.Alias(x => x.AlteradoEm)}", DateTime.Now }
            };

            Conexao.Sessao.Execute(
                sqlString, sqlParam, Conexao.Transicao);
        }
    }
}
