using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Aplicacao.Interceptadores
{
    public class UsuarioInter : Comum.BaseInterceptador
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
