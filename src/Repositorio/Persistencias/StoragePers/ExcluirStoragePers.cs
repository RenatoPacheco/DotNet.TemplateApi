using System;
using Dapper;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.FormatoJson;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;
using System.Linq;

namespace DotNetCore.API.Template.Repositorio.Persistencias.StoragePers
{
    internal class ExcluirStoragePers : Comum.SimplesRep
    {
        public ExcluirStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();
            StorageJson json = new StorageJson();

            string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                            [{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}] = @Status
                    WHERE {json.Coluna(x => x.Id)} IN @Id
                    OR {json.Coluna(x => x.Alias)} IN @Alias
                ";

            object sqlObject = new
            {
                Id = comando.Storage.Select(x => (long)x).ToList(),
                Alias = comando.Alias,
                Status = new DbString { Value = StatusAdapt.EnumParaSql(Status.Excluido), IsAnsi = true },
                AlteradoEm = DateTime.Now
            };

            Conexao.Sessao.Execute(
                sqlString, sqlObject, Conexao.Transicao);
        }
    }
}
