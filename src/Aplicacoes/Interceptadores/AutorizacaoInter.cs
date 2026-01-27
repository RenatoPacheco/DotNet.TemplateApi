using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Aplicacoes.Interceptadores {
    public class AutorizacaoInter : Comum.BaseInterceptador {
        public AutorizacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
