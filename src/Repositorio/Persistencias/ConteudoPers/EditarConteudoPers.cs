using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Mapeamentos;

namespace TemplateApi.Repositorio.Persistencias.ConteudoPers
{
    internal class EditarConteudoPers : Comum.SimplesRepositorio
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
            ConteudoMap map = new ConteudoMap();

            IsValid(dados);

            _persEhUnicoConteudo.EhUnico(dados);
            IsValid(_persEhUnicoConteudo);

            if (IsValid())
            {
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    UPDATE {map.Tabela} SET
                            {map.Col(x => x.Titulo)} = @Titulo
                           ,{map.Col(x => x.Alias)} = @Alias
                           ,{map.Col(x => x.Texto)} = @Texto
                           ,{map.Col(x => x.AlteradoEm)} = @AlteradoEm
                           ,{map.Col(x => x.Status)} = @Status
                    WHERE {map.Col(x => x.Id)} = @Id
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
