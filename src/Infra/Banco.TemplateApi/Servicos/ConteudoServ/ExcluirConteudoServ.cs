using System;
using Dapper;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Infra.Banco.TemplateApi.Servicos.ConteudoServ
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
                            {map.Col(x => x.AlteradoEm)} = @AlteradoEm
                           ,{map.Col(x => x.Status)} = @Status
                    WHERE {map.Col(x => x.Id)} IN @Id
                ";

            object sqlObject = new
            {
                Id = comando.Conteudo,
                Status = StatusAdapt.EnumParaSql(Status.Excluido),
                AlteradoEm = DateTime.Now
            };

            Conexao.Sessao.Execute(
                sqlString, sqlObject, Conexao.Transicao);
        }
    }
}
