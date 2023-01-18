using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Interceptadores
{
    public class AutorizacaoInter : Comum.BaseInterceptador
    {
        public AutorizacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
