using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class EditarUsuarioPers : Comum.SimplesRepositorio
    {
        public EditarUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoUsuarioPers persEhUnicoUsuario)
            : base(conexao, udt)
        {
            _persEhUnicoUsuario = persEhUnicoUsuario;
        }

        private readonly EhUnicoUsuarioPers _persEhUnicoUsuario;

        public void Editar(Usuario dados)
        {
            Notifications.Clear();
            UsuarioMap map = new UsuarioMap();

            IsValid(dados);

            _persEhUnicoUsuario.EhUnico(dados);
            IsValid(_persEhUnicoUsuario);

            if (IsValid())
            {
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE [dbo].[{map.Tabela}] SET
                            [{map.Col(x => x.Nome)}] = @Nome
                           ,[{map.Col(x => x.Email)}] = @Email
                           ,[{map.Col(x => x.Senha)}] = @Senha
                           ,[{map.Col(x => x.Telefone)}] = @Telefone
                           ,[{map.Col(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{map.Col(x => x.Status)}] = @Status
                    WHERE [{map.Col(x => x.Id)}] = @Id
                ";

                object sqlObject = new
                {
                    Id = dados.Id,
                    Nome = new DbString { Value = dados.Nome, IsAnsi = true },
                    Email = new DbString { Value = dados.Email, IsAnsi = true },
                    Senha = new DbString { Value = dados.Senha, IsAnsi = true },
                    Telefone = new DbString { Value = dados.Telefone, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(dados.Status), IsAnsi = true },
                    AlteradoEm = dados.AlteradoEm
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
