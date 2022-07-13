using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.Comandos.AutenticacaoCmds;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class AutenticacaoInter : Comum.BaseInter
    {
        public AutenticacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        internal void Iniciar(IniciarAutenticacaoCmd comando)
        {

        }
    }
}
