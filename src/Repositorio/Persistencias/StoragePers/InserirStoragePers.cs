using System;
using Dapper;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class InserirStoragePers
        : Comum.SimplesRepositorio
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

        public void Executar(Storage dados)
        {
            Notifications.Clear();
            StorageMap map = new StorageMap();

            IsValid(dados);

            _persEhUnicoStorage.Executar(dados);
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
                    Nome = dados.Nome,
                    Alias = dados.Alias,
                    Diretorio = dados.Diretorio,
                    Referencia = dados.Referencia,
                    Tipo = dados.Tipo,
                    Checksum = dados.Checksum,
                    Peso = dados.Peso,
                    Extensao = dados.Extensao,
                    Status = StatusAdapt.EnumParaSql(dados.Status),
                    CriadoEm = dados.CriadoEm,
                    AlteradoEm = dados.AlteradoEm
                };

                dados.Id = Conexao.Sessao.QuerySingle<int>(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
