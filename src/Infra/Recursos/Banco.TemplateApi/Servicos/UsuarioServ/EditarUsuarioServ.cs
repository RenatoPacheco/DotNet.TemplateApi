using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.UsuarioServ
{
    internal class EditarUsuarioServ
        : BaseSimplesServico
    {
        public EditarUsuarioServ(
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
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE {map.Tabela} SET
                            {map.Col(x => x.Nome)} = @{map.Alias(x => x.Nome)}
                           ,{map.Col(x => x.Email)} = @{map.Alias(x => x.Email)}
                           ,{map.Col(x => x.Senha)} = @{map.Alias(x => x.Senha)}
                           ,{map.Col(x => x.Telefone)} = @{map.Alias(x => x.Telefone)}
                           ,{map.Col(x => x.AlteradoEm)} = @{map.Alias(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)} = @{map.Alias(x => x.Status)}
                    WHERE {map.Col(x => x.Id)} = @{map.Alias(x => x.Id)}
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Id)}", dados.Id },
                    { $"{map.Alias(x => x.Nome)}", dados.Nome },
                    { $"{map.Alias(x => x.Email)}", dados.Email },
                    { $"{map.Alias(x => x.Senha)}", dados.Senha },
                    { $"{map.Alias(x => x.Telefone)}", dados.Telefone },
                    { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(dados.Status) },
                    { $"{map.Alias(x => x.AlteradoEm)}", dados.AlteradoEm }
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlParam, Conexao.Transicao);
            }
        }
    }
}
