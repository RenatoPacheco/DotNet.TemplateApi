using System;
using Dapper;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;

namespace TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.StorageServ
{
    internal class ExcluirStorageServ
        : BaseSimplesServico
    {
        public ExcluirStorageServ(
            Conexao conexao)
            : base(conexao) { }

        public void Executar(ExcluirStorageCmd comando)
        {
            Notifications.Clear();
            Mapeamentos.StorageMap map = new Mapeamentos.StorageMap();

            string sqlString = @$"
                    UPDATE {map.Tabela} SET
                            {map.Col(x => x.AlteradoEm)} = @AlteradoEm
                           ,{map.Col(x => x.Status)} = @Status
                    WHERE {map.Col(x => x.Id)} IN @Id
                    OR {map.Col(x => x.Alias)} IN @Alias
                ";

            object sqlObject = new
            {
                Id = comando.Storage,
                Alias = comando.Alias,
                Status = StatusAdapt.EnumParaSql(Status.Excluido),
                AlteradoEm = DateTime.Now
            };

            Conexao.Sessao.Execute(
                sqlString, sqlObject, Conexao.Transicao);
        }
    }
}
