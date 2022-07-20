using System;
using Dapper;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class EditarStoragePers : Comum.SimplesRepositorio
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
            StorageMap map = new StorageMap();

            IsValid(dados);

            _persEhUnicoStorage.EhUnico(dados);
            IsValid(_persEhUnicoStorage);

            if (IsValid())
            {
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE [dbo].[{map.Tabela}] SET
                            [{map.Col(x => x.Nome)}] = @Nome
                           ,[{map.Col(x => x.Diretorio)}] = @Diretorio
                           ,[{map.Col(x => x.Referencia)}] = @Referencia
                           ,[{map.Col(x => x.Tipo)}] = @Tipo
                           ,[{map.Col(x => x.Checksum)}] = @Checksum
                           ,[{map.Col(x => x.Peso)}] = @Peso
                           ,[{map.Col(x => x.Extensao)}] = @Extensao
                           ,[{map.Col(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{map.Col(x => x.Status)}] = @Status
                    WHERE [{map.Col(x => x.Id)}] = @Id
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
