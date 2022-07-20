using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class InserirConteudoPers : Comum.SimplesRepositorio
    {
        public InserirConteudoPers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoConteudoPers persEhUnicoConteudo)
            : base(conexao, udt)
        {
            _persEhUnicoConteudo = persEhUnicoConteudo;
        }

        private readonly EhUnicoConteudoPers _persEhUnicoConteudo;

        public void Inserir(Conteudo dados)
        {
            Notifications.Clear();
            ConteudoMap json = new ConteudoMap();

            Validate(dados);

            _persEhUnicoConteudo.EhUnico(dados);
            Validate(_persEhUnicoConteudo);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO [dbo].[{json.Tabela}]
                           ([{json.Coluna(x => x.Titulo)}]
                           ,[{json.Coluna(x => x.Alias)}]
                           ,[{json.Coluna(x => x.Texto)}]
                           ,[{json.Coluna(x => x.CriadoEm)}]
                           ,[{json.Coluna(x => x.AlteradoEm)}]
                           ,[{json.Coluna(x => x.Status)}])
                    VALUES
                           (@Titulo
                           ,@Alias
                           ,@Texto
                           ,@CriadoEm
                           ,@AlteradoEm
                           ,@Status); 
                    SELECT CAST(SCOPE_IDENTITY() as int);
                ";

                object sqlObject = new
                {
                    Titulo = new DbString { Value = dados.Titulo, IsAnsi = true },
                    Alias = new DbString { Value = dados.Alias, IsAnsi = true },
                    Texto = new DbString { Value = dados.Texto, IsAnsi = true },
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
