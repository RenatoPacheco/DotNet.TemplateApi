using System;
using Dapper;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.ConteudoServ
{
    internal class EditarConteudoServ
        : BaseSimplesServico
    {
        public EditarConteudoServ(
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
                    Titulo = dados.Titulo,
                    Alias = dados.Alias,
                    Texto = dados.Texto,
                    Status = StatusAdapt.EnumParaSql(dados.Status),
                    AlteradoEm = dados.AlteradoEm
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlObject, Conexao.Transicao);
            }
        }
    }
}
