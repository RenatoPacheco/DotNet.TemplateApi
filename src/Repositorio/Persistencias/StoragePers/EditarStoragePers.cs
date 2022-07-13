using System;
using Dapper;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.FormatoJson;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Repositorio.Persistencias.StoragePers
{
    internal class EditarStoragePers : Comum.SimplesRep
    {
        public EditarStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoStoragePers persEhUnicoStorage)
            : base(conexao, udt)
        {
            _persEhUnicoStorage = persEhUnicoStorage;
        }

        private readonly EhUnicoStoragePers _persEhUnicoStorage;

        public void Editar(Storage dados)
        {
            Notifications.Clear();
            StorageJson json = new StorageJson();

            Validate(dados);

            _persEhUnicoStorage.EhUnico(dados);
            Validate(_persEhUnicoStorage);

            if (IsValid())
            {
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                            [{json.Coluna(x => x.Nome)}] = @Nome
                           ,[{json.Coluna(x => x.Diretorio)}] = @Diretorio
                           ,[{json.Coluna(x => x.Referencia)}] = @Referencia
                           ,[{json.Coluna(x => x.Tipo)}] = @Tipo
                           ,[{json.Coluna(x => x.Checksum)}] = @Checksum
                           ,[{json.Coluna(x => x.Peso)}] = @Peso
                           ,[{json.Coluna(x => x.Extensao)}] = @Extensao
                           ,[{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}] = @Status
                    WHERE [{json.Coluna(x => x.Id)}] = @Id
                ";

                object sqlObject = new
                {
                    Id = dados.Id,
                    Nome = new DbString { Value = dados.Nome, IsAnsi = true },
                    Diretorio = new DbString { Value = dados.Diretorio, IsAnsi = true },
                    Referencia = new DbString { Value = dados.Referencia, IsAnsi = true },
                    Tipo = new DbString { Value = dados.Tipo, IsAnsi = true },
                    Checksum = new DbString { Value = dados.Checksum, IsAnsi = true },
                    Peso = dados.Peso,
                    Extensao = new DbString { Value = dados.Extensao, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(dados.Status), IsAnsi = true },
                    AlteradoEm = dados.AlteradoEm
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
