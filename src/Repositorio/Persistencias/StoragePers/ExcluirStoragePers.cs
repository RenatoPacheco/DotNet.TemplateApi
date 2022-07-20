using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class ExcluirStoragePers : Comum.SimplesRepositorio
    {
        public ExcluirStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();
            StorageMap map = new StorageMap();

            string sqlString = @$"
                    UPDATE [dbo].[{map.Tabela}] SET
                            [{map.Col(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{map.Col(x => x.Status)}] = @Status
                    WHERE {map.Col(x => x.Id)} IN @Id
                    OR {map.Col(x => x.Alias)} IN @Alias
                ";

            object sqlObject = new
            {
                Id = comando.Storage,
                Alias = comando.Alias,
                Status = new DbString { Value = StatusAdapt.EnumParaSql(Status.Excluido), IsAnsi = true },
                AlteradoEm = DateTime.Now
            };

            Conexao.Sessao.Execute(
                sqlString, sqlObject, Conexao.Transicao);
        }
    }
}
