using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Comandos.AutenticacaoCmds;

namespace TemplateApi.Aplicacao.Interceptadores
{
    public class AutenticacaoInter : Comum.BaseInterceptador
    {
        public AutenticacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        internal void Iniciar(IniciarAutenticacaoCmd comando)
        {

        }
    }
}
