using TemplateApi.Dominio.Entidades;
using TemplateApi.Infra.Banco.TemplateApi.Servicos.UsuarioServ;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class InserirUsuarioPers
        : Comum.BaseRepositorio
    {
        public InserirUsuarioPers(
            InserirUsuarioServ servInserirUsuario)
        {
            _servInserirUsuario = servInserirUsuario;
        }

        private readonly InserirUsuarioServ _servInserirUsuario;

        public void Executar(Usuario dados)
        {
            Notifications.Clear();

            _servInserirUsuario.Executar(dados);
            IsValid(_servInserirUsuario);
        }
    }
}
