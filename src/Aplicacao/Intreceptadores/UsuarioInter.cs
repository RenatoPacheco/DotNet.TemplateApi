using DotNetCore.API.Template.Dominio.Servicos;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class UsuarioInter : Comum.BaseInter
    {
        public UsuarioInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }
    }
}
