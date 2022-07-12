using System;
using Dapper;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.FormatoJson;

namespace DotNetCore.API.Template.Repositorio.Persistencias.UsuarioPers
{
    internal class InserirUsuarioPers : Comum.SimplesRep
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
            UsuarioJson json = new UsuarioJson();

            Validate(dados);

            _persEhUnicoUsuario.EhUnico(dados);
            Validate(_persEhUnicoUsuario);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO [dbo].[{json.Tabela}]
                           ([{json.Coluna(x => x.Nome)}]
                           ,[{json.Coluna(x => x.Email)}]
                           ,[{json.Coluna(x => x.Senha)}]
                           ,[{json.Coluna(x => x.Telefone)}]
                           ,[{json.Coluna(x => x.CriadoEm)}]
                           ,[{json.Coluna(x => x.AlteradoEm)}]
                           ,[{json.Coluna(x => x.Status)}])
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
