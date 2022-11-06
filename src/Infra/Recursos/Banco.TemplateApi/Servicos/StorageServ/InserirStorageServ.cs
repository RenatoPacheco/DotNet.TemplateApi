using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.StorageServ
{
    internal class InserirStorageServ
        : BaseSimplesServico
    {
        public InserirStorageServ(
            Conexao conexao,
            EhUnicoStorageServ persEhUnicoStorage)
            : base(conexao)
        {
            _persEhUnicoStorage = persEhUnicoStorage;
        }

        private readonly EhUnicoStorageServ _persEhUnicoStorage;

        public void Executar(Storage dados)
        {
            Notifications.Clear();
            Mapeamentos.StorageMap map = new Mapeamentos.StorageMap();

            IsValid(dados);

            _persEhUnicoStorage.Executar(dados);
            IsValid(_persEhUnicoStorage);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = $@"
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
                           (@{map.Alias(x => x.Nome)}
                           ,@{map.Alias(x => x.Alias)}
                           ,@{map.Alias(x => x.Diretorio)}
                           ,@{map.Alias(x => x.Referencia)}
                           ,@{map.Alias(x => x.Tipo)}
                           ,@{map.Alias(x => x.Checksum)}
                           ,@{map.Alias(x => x.Peso)}
                           ,@{map.Alias(x => x.Extensao)}
                           ,@{map.Alias(x => x.CriadoEm)}
                           ,@{map.Alias(x => x.AlteradoEm)}
                           ,@{map.Alias(x => x.Status)}); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Nome)}", dados.Nome },
                    { $"{map.Alias(x => x.Alias)}", dados.Alias },
                    { $"{map.Alias(x => x.Diretorio)}", dados.Diretorio },
                    { $"{map.Alias(x => x.Referencia)}", dados.Referencia },
                    { $"{map.Alias(x => x.Tipo)}", dados.Tipo },
                    { $"{map.Alias(x => x.Checksum)}", dados.Checksum },
                    { $"{map.Alias(x => x.Peso)}", dados.Peso },
                    { $"{map.Alias(x => x.Extensao)}", dados.Extensao },
                    { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(dados.Status) },
                    { $"{map.Alias(x => x.CriadoEm)}", dados.CriadoEm },
                    { $"{map.Alias(x => x.AlteradoEm)}", dados.AlteradoEm }
                };

                dados.Id = Conexao.Sessao.QuerySingle<int>(
                    sqlString, sqlParam, Conexao.Transicao);
            }
        }
    }
}
