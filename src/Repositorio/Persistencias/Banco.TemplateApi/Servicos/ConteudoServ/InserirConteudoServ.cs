using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Adaptadores;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio.Persistencias.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class InserirConteudoServ
        : Comum.SimplesRepositorio
    {
        public InserirConteudoServ(
            Conexao conexao,
            IUnidadeTrabalho udt,
            EhUnicoConteudoServ persEhUnicoConteudo)
            : base(conexao, udt)
        {
            _persEhUnicoConteudo = persEhUnicoConteudo;
        }

        private readonly EhUnicoConteudoServ _persEhUnicoConteudo;

        public void Inserir(Conteudo dados)
        {
            Notifications.Clear();
            Mapeamentos.ConteudoMap map = new Mapeamentos.ConteudoMap();

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
