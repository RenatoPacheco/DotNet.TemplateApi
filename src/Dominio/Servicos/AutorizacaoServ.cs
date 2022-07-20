using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;

namespace TemplateApi.Dominio.Servicos
{
    public class AutorizacaoServ : Comum.BaseServico
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
