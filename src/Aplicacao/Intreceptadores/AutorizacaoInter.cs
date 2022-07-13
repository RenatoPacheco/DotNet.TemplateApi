using DotNetCore.API.Template.Dominio.Servicos;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class AutorizacaoInter : Comum.BaseInter
    {
        public AutorizacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
