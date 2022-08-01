using System;
using Dapper;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class ExcluirStoragePers
        : Comum.SimplesRepositorio
    {
        public ExcluirStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Executar(ExcluirStorageCmd comando)
        {
            Notifications.Clear();
            StorageMap map = new StorageMap();

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
