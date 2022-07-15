using System;
using Dapper;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.MapeamentoSql;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds;
using System.Linq;

namespace DotNetCore.API.Template.Repositorio.Persistencias.ConteudoPers
{
    internal class ExcluirConteudoPers : Comum.SimplesRep
    {
        public ExcluirConteudoPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();
            ConteudoMapSql json = new ConteudoMapSql();

            string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                            [{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}] = @Status
                    WHERE {json.Coluna(x => x.Id)} IN @Id
                ";

            object sqlObject = new
            {
                Id = comando.Conteudo,
                Status = new DbString { Value = StatusAdapt.EnumParaSql(Status.Excluido), IsAnsi = true },
                AlteradoEm = DateTime.Now
            };

            Conexao.Sessao.Execute(
                sqlString, sqlObject, Conexao.Transicao);
        }
    }
}
