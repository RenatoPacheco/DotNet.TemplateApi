using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class InserirConteudoServ
        : BaseSimplesServico
    {
        public InserirConteudoServ(
            Conexao conexao,
            EhUnicoConteudoServ persEhUnicoConteudo)
            : base(conexao)
        {
            _persEhUnicoConteudo = persEhUnicoConteudo;
        }

        private readonly EhUnicoConteudoServ _persEhUnicoConteudo;

        public void Executar(Conteudo dados)
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
