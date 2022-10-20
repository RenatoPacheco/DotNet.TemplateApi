using System;
using Dapper;
using TemplateApi.Infra.Adaptadores;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using System.Collections.Generic;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.StorageServ
{
    internal class ExcluirStorageServ
        : BaseSimplesServico
    {
        public ExcluirStorageServ(
            Conexao conexao)
            : base(conexao) { }

        public void Executar(ExcluirStorageCmd comando)
        {
            Notifications.Clear();
            Mapeamentos.StorageMap map = new Mapeamentos.StorageMap();

            string sqlString = @$"
                    UPDATE {map.Tabela} SET
                            {map.Col(x => x.AlteradoEm)} = @{map.Alias(x => x.AlteradoEm)}
                           ,{map.Col(x => x.Status)} = @{map.Alias(x => x.Status)}
                    WHERE {map.Col(x => x.Id)} IN @{map.Alias(x => x.Id)}
                    OR {map.Col(x => x.Alias)} IN @{map.Alias(x => x.Alias)}
                ";

            IDictionary<string, object> sqlParam = new Dictionary<string, object>
            {
                { $"{map.Alias(x => x.Id)}", comando.Storage },
                { $"{map.Alias(x => x.Alias)}", comando.Alias },
                { $"{map.Alias(x => x.Status)}", StatusAdapt.EnumParaSql(Status.Excluido) },
                { $"{map.Alias(x => x.AlteradoEm)}", DateTime.Now }
            };

            Conexao.Sessao.Execute(
                sqlString, sqlParam, Conexao.Transicao);
        }
    }
}
