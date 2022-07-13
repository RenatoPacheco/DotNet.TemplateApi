using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;

namespace DotNetCore.API.Template.Aplicacao.Intreceptadores
{
    public class UsuarioInter : Comum.BaseInter
    {
        public UsuarioInter(
            AutenticacaoServ servAutenticacao)
            : base(servAutenticacao) { }

        internal void Excluir(ExcluirUsuarioCmd comando)
        {

        }

        internal void Editar(EditarUsuarioCmd comando)
        {

        }

        internal void Inserir(InserirUsuarioCmd comando)
        {

        }

        internal void Filtrar(FiltrarUsuarioCmd comando)
        {

        }
    }
}
