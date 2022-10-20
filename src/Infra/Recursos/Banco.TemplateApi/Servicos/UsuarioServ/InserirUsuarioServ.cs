using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.UsuarioServ
{
    internal class InserirUsuarioServ
        : BaseSimplesServico
    {
        public InserirUsuarioServ(
            Conexao conexao,
            EhUnicoUsuarioServ persEhUnicoUsuario)
            : base(conexao)
        {
            _persEhUnicoUsuario = persEhUnicoUsuario;
        }

        private readonly EhUnicoUsuarioServ _persEhUnicoUsuario;

        public void Executar(Usuario dados)
        {
            Notifications.Clear();
            Mapeamentos.UsuarioMap map = new Mapeamentos.UsuarioMap();

            IsValid(dados);

            _persEhUnicoUsuario.Executar(dados);
            IsValid(_persEhUnicoUsuario);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO {map.Tabela}
                           ({map.Col(x => x.Nome)}
                           ,{map.Col(x => x.Email)}
                           ,{map.Col(x => x.Senha)}
                           ,{map.Col(x => x.Telefone)}
                           ,{map.Col(x => x.CriadoEm)}
                           ,{map.Col(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)})
                    VALUES
                           (@{map.Alias(x => x.Nome)}
                           ,@{map.Alias(x => x.Email)}
                           ,@{map.Alias(x => x.Senha)}
                           ,@{map.Alias(x => x.Telefone)}
                           ,@{map.Alias(x => x.CriadoEm)}
                           ,@{map.Alias(x => x.AlteradoEm)}
                           ,@{map.Alias(x => x.Status)}); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Nome)}", dados.Nome },
                    { $"{map.Alias(x => x.Email)}", dados.Email },
                    { $"{map.Alias(x => x.Senha)}", dados.Senha },
                    { $"{map.Alias(x => x.Telefone)}", dados.Telefone },
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
