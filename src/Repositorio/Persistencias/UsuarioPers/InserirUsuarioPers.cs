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
            UsuarioMap json = new UsuarioMap();

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
