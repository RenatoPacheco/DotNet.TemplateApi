using System;
using Dapper;
using System.Collections.Generic;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.UsuarioServ
{
    internal class ExcluirUsuarioServ
        : BaseSimplesServico
    {
        public ExcluirUsuarioServ(
            Conexao conexao)
            : base(conexao) { }

        public void Executar(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();
            Mapeamentos.UsuarioMap map = new Mapeamentos.UsuarioMap();

            string sqlString = @$"
                    UPDATE {map.Tabela} SET
                            {map.Col(x => x.AlteradoEm)} = @{map.Alias(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)} = @{map.Alias(x => x.Status)}
                    WHERE {map.Col(x => x.Id)} IN @{map.Alias(x => x.Id)}
                ";

            IDictionary<string, object> sqlParam = new Dictionary<string, object>
            {
                { $"{map.Alias(x => x.Id)}", comando.Usuario },
                { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(Status.Excluido) },
                { $"{map.Alias(x => x.AlteradoEm)}", DateTime.Now }
            };

            Conexao.Sessao.Execute(
                sqlString, sqlParam, Conexao.Transicao);
        }
    }
}
