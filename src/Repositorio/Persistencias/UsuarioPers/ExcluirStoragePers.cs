using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.UsuarioServ;

namespace TemplateApi.Repositorio.Persistencias.UsuarioPers
{
    internal class ExcluirUsuarioPers
        : Comum.BaseRepositorio
    {
        public ExcluirUsuarioPers(
            ExcluirUsuarioServ servExcluirUsuario)
        {
            _servExcluirUsuario = servExcluirUsuario;
        }

        private readonly ExcluirUsuarioServ _servExcluirUsuario;

        public void Executar(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();

            _servExcluirUsuario.Executar(comando);
            IsValid(_servExcluirUsuario);
        }
    }
}
