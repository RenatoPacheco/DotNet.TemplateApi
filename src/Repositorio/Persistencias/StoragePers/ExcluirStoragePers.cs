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
            StorageMap json = new StorageMap();

            string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                            [{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}] = @Status
                    WHERE {json.Coluna(x => x.Id)} IN @Id
                    OR {json.Coluna(x => x.Alias)} IN @Alias
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
