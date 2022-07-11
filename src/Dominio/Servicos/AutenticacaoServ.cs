using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class AutenticacaoServ : Comum.BaseServ
    {
        public AutenticacaoServ(
            IAutorizacaoRep repAutorizacao)
        {
            _repAutorizacao = repAutorizacao;
        }

        protected readonly IAutorizacaoRep _repAutorizacao;

        public Autenticacao Obter()
        {
            Notifications.Clear();

            Autenticacao resultado = Autenticacao.GerarInterno();
            resultado.Autorizacoes = _repAutorizacao.Listar();

            return resultado;
        }
    }
}
