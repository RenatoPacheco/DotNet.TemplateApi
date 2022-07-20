using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacao.Intreceptadores
{
    public class AutorizacaoInter : Comum.BaseInter
    {
        public AutorizacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
