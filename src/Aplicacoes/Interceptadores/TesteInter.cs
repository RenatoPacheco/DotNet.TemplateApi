using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Comandos.TesteCmds;

namespace TemplateApi.Aplicacoes.Interceptadores {
    public class TesteInter : Comum.BaseInterceptador {
        public TesteInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        internal void Formatos(FormatosTesteCmd comando) {

        }
    }
}
