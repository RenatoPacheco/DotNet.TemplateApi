using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class InserirUsuarioPers : Comum.SimplesRepositorio
    {
        public InserirUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoUsuarioPers persEhUnicoUsuario)
            : base(conexao, udt)
        {
            _persEhUnicoUsuario = persEhUnicoUsuario;
        }

        private readonly EhUnicoUsuarioPers _persEhUnicoUsuario;

        public void Inserir(Usuario dados)
        {
            Notifications.Clear();
            UsuarioMap map = new UsuarioMap();

            IsValid(dados);

            _persEhUnicoUsuario.EhUnico(dados);
            IsValid(_persEhUnicoUsuario);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO [dbo].[{map.Tabela}]
                           ([{map.Col(x => x.Nome)}]
                           ,[{map.Col(x => x.Email)}]
                           ,[{map.Col(x => x.Senha)}]
                           ,[{map.Col(x => x.Telefone)}]
                           ,[{map.Col(x => x.CriadoEm)}]
                           ,[{map.Col(x => x.AlteradoEm)}]
                           ,[{map.Col(x => x.Status)}])
                    VALUES
                           (@Nome
                           ,@Email
                           ,@Senha
                           ,@Telefone
                           ,@CriadoEm
                           ,@AlteradoEm
                           ,@Status); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                object sqlObject = new
                {
                    Nome = new DbString { Value = dados.Nome, IsAnsi = true },
                    Email = new DbString { Value = dados.Email, IsAnsi = true },
                    Senha = new DbString { Value = dados.Senha, IsAnsi = true },
                    Telefone = new DbString { Value = dados.Telefone, IsAnsi = true },
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
