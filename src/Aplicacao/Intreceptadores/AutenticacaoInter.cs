using DotNetCore.API.Template.Dominio.Servicos;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class AutenticacaoInter : Comum.BaseInter
    {
        public AutenticacaoInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
