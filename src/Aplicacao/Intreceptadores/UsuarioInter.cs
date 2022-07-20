using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Aplicacao.Intreceptadores
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
