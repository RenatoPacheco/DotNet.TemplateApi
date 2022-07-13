using DotNetCore.API.Template.Dominio.Servicos;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class SobreInter : Comum.BaseInter
    {
        public SobreInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
