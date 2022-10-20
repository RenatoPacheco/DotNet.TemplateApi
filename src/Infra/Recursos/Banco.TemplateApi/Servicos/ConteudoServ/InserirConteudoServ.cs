using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class InserirConteudoServ
        : BaseSimplesServico
    {
        public InserirConteudoServ(
            Conexao conexao,
            EhUnicoConteudoServ persEhUnicoConteudo)
            : base(conexao)
        {
            _persEhUnicoConteudo = persEhUnicoConteudo;
        }

        private readonly EhUnicoConteudoServ _persEhUnicoConteudo;

        public void Executar(Conteudo dados)
        {
            Notifications.Clear();
            Mapeamentos.ConteudoMap map = new Mapeamentos.ConteudoMap();

            IsValid(dados);

            _persEhUnicoConteudo.Executar(dados);
            IsValid(_persEhUnicoConteudo);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO {map.Tabela}
                           ({map.Col(x => x.Titulo)}
                           ,{map.Col(x => x.Alias)}
                           ,{map.Col(x => x.Texto)}
                           ,{map.Col(x => x.CriadoEm)}
                           ,{map.Col(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)})
                    VALUES
                           (@{map.Alias(x => x.Titulo)}
                           ,@{map.Alias(x => x.Alias)}
                           ,@{map.Alias(x => x.Texto)}
                           ,@{map.Alias(x => x.CriadoEm)}
                           ,@{map.Alias(x => x.AlteradoEm)}
                           ,@{map.Alias(x => x.Status)}); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Titulo)}", dados.Titulo },
                    { $"{map.Alias(x => x.Alias)}", dados.Alias },
                    { $"{map.Alias(x => x.Texto)}", dados.Texto },
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
