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
            ConteudoMap map = new ConteudoMap();

            IsValid(dados);

            _persEhUnicoConteudo.Executar(dados);
            IsValid(_persEhUnicoConteudo);

            if (IsValid())
            {
                dados.CriadoEm = DateTime.Now;
                dados.AlteradoEm = DateTime.Now;

                string sqlString = @$"
                    INSERT INTO {map.Tabela}
                           ({map.Col(x => x.Titulo)}
                           ,{map.Col(x => x.Alias)}
                           ,{map.Col(x => x.Texto)}
                           ,{map.Col(x => x.CriadoEm)}
                           ,{map.Col(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)})
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
                    Titulo = dados.Titulo,
                    Alias = dados.Alias,
                    Texto = dados.Texto,
                    Status = StatusAdapt.EnumParaSql(dados.Status),
                    CriadoEm = dados.CriadoEm,
                    AlteradoEm = dados.AlteradoEm
                };

                dados.Id = Conexao.Sessao.QuerySingle<int>(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
