using System;
using Dapper;
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
                            {map.Col(x => x.AlteradoEm)} = @AlteradoEm
                           ,{map.Col(x => x.Status)} = @Status
                    WHERE {map.Col(x => x.Id)} IN @Id
                ";

            object sqlObject = new
            {
                Id = comando.Usuario,
                Status = StatusAdapt.EnumParaSql(Status.Excluido),
                AlteradoEm = DateTime.Now
            };

            Conexao.Sessao.Execute(
                sqlString, sqlObject, Conexao.Transicao);
        }
    }
}
