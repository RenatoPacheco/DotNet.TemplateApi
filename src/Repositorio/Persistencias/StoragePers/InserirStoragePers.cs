using System;
using Dapper;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class InserirStoragePers : Comum.SimplesRepositorio
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
            StorageMap map = new StorageMap();

            IsValid(dados);

            _persEhUnicoStorage.EhUnico(dados);
            IsValid(_persEhUnicoStorage);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO {map.Tabela}
                           ({map.Col(x => x.Nome)}
                           ,{map.Col(x => x.Alias)}
                           ,{map.Col(x => x.Diretorio)}
                           ,{map.Col(x => x.Referencia)}
                           ,{map.Col(x => x.Tipo)}
                           ,{map.Col(x => x.Checksum)}
                           ,{map.Col(x => x.Peso)}
                           ,{map.Col(x => x.Extensao)}
                           ,{map.Col(x => x.CriadoEm)}
                           ,{map.Col(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)})
                    VALUES
                           (@Nome
                           ,@Alias
                           ,@Diretorio
                           ,@Referencia
                           ,@Tipo
                           ,@Checksum
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
                    Checksum = new DbString { Value = dados.Checksum, IsAnsi = true },
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
