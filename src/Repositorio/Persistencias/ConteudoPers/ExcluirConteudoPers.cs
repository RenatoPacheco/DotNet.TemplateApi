using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using System.Linq;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class ExcluirConteudoPers : Comum.SimplesRepositorio
    {
        public ExcluirConteudoPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Excluir(ExcluirConteudoCmd comando)
        {
            Notifications.Clear();
            ConteudoMap map = new ConteudoMap();

            string sqlString = @$"
                    UPDATE [dbo].[{map.Tabela}] SET
                            [{map.Col(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{map.Col(x => x.Status)}] = @Status
                    WHERE {map.Col(x => x.Id)} IN @Id
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
