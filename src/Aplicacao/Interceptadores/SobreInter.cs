using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Interceptadores
{
    public class SobreInter : Comum.BaseInterceptador
    {
        public SobreInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
