using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Intreceptadores
{
    public class SobreInter : Comum.BaseInterceptador
    {
        public SobreInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
