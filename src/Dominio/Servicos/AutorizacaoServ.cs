using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class AutorizacaoServ : Comum.BaseServ
    {
        public AutorizacaoServ(
            IAutorizacaoRep repAutorizacao)
        {
            _repAutorizacao = repAutorizacao;
        }

        protected readonly IAutorizacaoRep _repAutorizacao;

        public Autorizacao[] Listar()
        {
            Notifications.Clear();
            Autorizacao[] resultado = _repAutorizacao.Listar();
            Validate(_repAutorizacao);

            return resultado;
        }
    }
}
