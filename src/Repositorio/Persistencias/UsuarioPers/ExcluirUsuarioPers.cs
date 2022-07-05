using System;
using Dapper;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Repositorio.Adaptadores;
using DotNetCore.API.Template.Repositorio.FormatoJson;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Repositorio.Persistencias.UsuarioPers
{
    internal class ExcluirUsuarioPers : Comum.SimplesRep
    {
        public ExcluirUsuarioPers(
            Conexao conexao,
            IUnidadeTrabalho udt)
            : base(conexao, udt) { }

        public void Excluir(int dados)
        {
            Excluir(new int[] { dados });
        }

        public void Excluir(IEnumerable<int> dados)
        {
            Notifications.Clear();
            UsuarioJson json = new UsuarioJson();

            if (IsValid())
            {
                string sqlString = @$"
                    UPDATE [dbo].[{json.Tabela}] SET
                           ,[{json.Coluna(x => x.AlteradoEm)}] = @AlteradoEm
                           ,[{json.Coluna(x => x.Status)}]) = @Status
                    WHERE {json.Coluna(x => x.Id)} IN @Id
                ";

                object sqlObject = new
                {
                    Id = dados,
                    Status = new DbString { Value = StatusAdapt.EnumParaSql(Status.Excluido), IsAnsi = true },
                    AlteradoEm = DateTime.Now
                };

                Conexao.Sessao.QuerySingle<int>(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
