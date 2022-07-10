using System;
using Dapper;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.FormatoJson;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Repositorio.Persistencias.StoragePers
{
    internal class InserirStoragePers : Comum.SimplesRep
    {
        public InserirStoragePers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoStoragePers persEhUnicoStorage)
            : base(conexao, udt)
        {
            _persEhUnicoStorage = persEhUnicoStorage;
        }

        private readonly EhUnicoStoragePers _persEhUnicoStorage;

        public void Inserir(Storage dados)
        {
            Notifications.Clear();
            StorageJson json = new StorageJson();

            Validate(dados);

            _persEhUnicoStorage.EhUnico(dados);
            Validate(_persEhUnicoStorage);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO [dbo].[{json.Tabela}]
                           ([{json.Coluna(x => x.Nome)}]
                           ,[{json.Coluna(x => x.Alias)}]
                           ,[{json.Coluna(x => x.Diretorio)}]
                           ,[{json.Coluna(x => x.Referencia)}]
                           ,[{json.Coluna(x => x.Tipo)}]
                           ,[{json.Coluna(x => x.Peso)}]
                           ,[{json.Coluna(x => x.Extensao)}]
                           ,[{json.Coluna(x => x.CriadoEm)}]
                           ,[{json.Coluna(x => x.AlteradoEm)}]
                           ,[{json.Coluna(x => x.Status)}])
                    VALUES
                           (@Nome
                           ,@Alias
                           ,@Diretorio
                           ,@Referencia
                           ,@Tipo
                           ,@Peso
                           ,@Extensao
                           ,@CriadoEm
                           ,@AlteradoEm
                           ,@Status); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                object sqlObject = new
                {
                    Nome = new DbString { Value = dados.Nome, IsAnsi = true },
                    Alias = new DbString { Value = dados.Alias, IsAnsi = true },
                    Diretorio = new DbString { Value = dados.Diretorio, IsAnsi = true },
                    Referencia = new DbString { Value = dados.Referencia, IsAnsi = true },
                    Tipo = new DbString { Value = dados.Tipo, IsAnsi = true },
                    Peso = dados.Peso,
                    Extensao = new DbString { Value = dados.Extensao, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(dados.Status), IsAnsi = true },
                    CriadoEm = dados.CriadoEm,
                    AlteradoEm = dados.AlteradoEm
                };

                dados.Id = Conexao.Sessao.QuerySingle<int>(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
