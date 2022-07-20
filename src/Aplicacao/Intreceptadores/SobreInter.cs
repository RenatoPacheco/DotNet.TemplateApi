using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Intreceptadores
{
    public class SobreInter : Comum.BaseInter
    {
        public SobreInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
