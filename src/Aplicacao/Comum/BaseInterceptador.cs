using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Comum
{
    public abstract class BaseInterceptador
        : Dominio.Servicos.Comum.BaseServico
    {
        public BaseInterceptador(
            AutenticacaoServ servAutenticacao)
        {
            _servAutenticacao = servAutenticacao;
        }

        protected readonly AutenticacaoServ _servAutenticacao;
    }
}