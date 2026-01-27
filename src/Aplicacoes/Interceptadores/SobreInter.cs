using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacoes.Interceptadores {
    public class SobreInter : Comum.BaseInterceptador {
        public SobreInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
