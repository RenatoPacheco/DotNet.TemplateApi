using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Banco.TemplateApi.Servicos.UsuarioServ;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class EditarUsuarioPers
        : Comum.BaseRepositorio
    {
        public EditarUsuarioPers(
            EditarUsuarioServ servEditarUsuario)
        {
            _servEditarUsuario = servEditarUsuario;
        }

        private readonly EditarUsuarioServ _servEditarUsuario;

        public void Executar(Usuario dados)
        {
            Notifications.Clear();

            _servEditarUsuario.Executar(dados);
            IsValid(_servEditarUsuario);
        }
    }
}
