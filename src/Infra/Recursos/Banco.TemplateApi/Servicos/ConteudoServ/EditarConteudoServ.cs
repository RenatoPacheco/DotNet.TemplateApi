using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Adaptadores;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.ConteudoServ
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
                            {map.Col(x => x.Titulo)} = @{map.Alias(x => x.Titulo)}
                           ,{map.Col(x => x.Alias)} = @{map.Alias(x => x.Alias)}
                           ,{map.Col(x => x.Texto)} = @{map.Alias(x => x.Texto)}
                           ,{map.Col(x => x.AlteradoEm)} = @{map.Alias(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)} = @{map.Alias(x => x.Status)}
                    WHERE {map.Col(x => x.Id)} = @Id
                ";

                IDictionary<string, object> sqlParam = new Dictionary<string, object>
                {
                    { $"{map.Alias(x => x.Id)}", dados.Id },
                    { $"{map.Alias(x => x.Titulo)}", dados.Titulo },
                    { $"{map.Alias(x => x.Alias)}", dados.Alias },
                    { $"{map.Alias(x => x.Texto)}", dados.Texto },
                    { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(dados.Status) },
                    { $"{map.Alias(x => x.AlteradoEm)}", dados.AlteradoEm }
                };

                Conexao.Sessao.Execute(
                    sqlString, sqlParam, Conexao.Transicao);
            }
        }
    }
}
