using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Intreceptadores
{
    public class AutorizacaoInter : Comum.BaseInterceptador
    {
        public AutorizacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
