using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.UsuarioServ
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
                            {map.Col(x => x.Nome)} = @Nome
                           ,{map.Col(x => x.Email)} = @Email
                           ,{map.Col(x => x.Senha)} = @Senha
                           ,{map.Col(x => x.Telefone)} = @Telefone
                           ,{map.Col(x => x.AlteradoEm)} = @AlteradoEm
                           ,{map.Col(x => x.Status)} = @Status
                    WHERE {map.Col(x => x.Id)} = @Id
                ";

                object sqlObject = new
                {
                    Id = dados.Id,
                    Nome = dados.Nome,
                    Email = dados.Email,
                    Senha = dados.Senha,
                    Telefone = dados.Telefone,
                    Status = StatusAdapt.EnumParaSql(dados.Status),
                    AlteradoEm = dados.AlteradoEm
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
