using System;
using Dapper;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.MapeamentoSql;

namespace DotNetCore.API.Template.Repositorio.Persistencias.ConteudoPers
{
    internal class EditarConteudoPers : Comum.SimplesRep
    {
        public EditarConteudoPers(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoConteudoPers persEhUnicoConteudo)
            : base(conexao, udt)
        {
            _persEhUnicoConteudo = persEhUnicoConteudo;
        }

        private readonly EhUnicoConteudoPers _persEhUnicoConteudo;

        public void Editar(Conteudo dados)
        {
            Notifications.Clear();
            ConteudoMapSql json = new ConteudoMapSql();

            Validate(dados);

            _persEhUnicoConteudo.EhUnico(dados);
            Validate(_persEhUnicoConteudo);

            if (IsValid())
            {
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                            [{json.Coluna(x => x.Titulo)}] = @Titulo
                           ,[{json.Coluna(x => x.Alias)}] = @Alias
                           ,[{json.Coluna(x => x.Texto)}] = @Texto
                           ,[{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}] = @Status
                    WHERE [{json.Coluna(x => x.Id)}] = @Id
                ";

                object sqlObject = new
                {
                    Id = dados.Id,
                    Titulo = new DbString { Value = dados.Titulo, IsAnsi = true },
                    Alias = new DbString { Value = dados.Alias, IsAnsi = true },
                    Texto = new DbString { Value = dados.Texto, IsAnsi = true },
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(dados.Status), IsAnsi = true },
                    AlteradoEm = dados.AlteradoEm
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
