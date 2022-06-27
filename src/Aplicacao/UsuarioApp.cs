using System;
using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;

namespace DotNetCore.API.Template.Aplicacao
{
    public class UsuarioApp : Comum.BaseApp
    {
        public UsuarioApp(
            UsuarioServ servUsuario)
        {
            _servUsuario = servUsuario;
        }

        protected readonly UsuarioServ _servUsuario;

        public Usuario[] Filtrar(FiltrarUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario[] resultado = Array.Empty<Usuario>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servUsuario.Filtrar(comando);
                Validate(_servUsuario);
            }

            return resultado;
        }

        public Usuario Inserir(InserirUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servUsuario.Inserir(comando);
                Validate(_servUsuario);
            }

            return resultado;
        }

        public Usuario Editar(EditarUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servUsuario.Editar(comando);
                Validate(_servUsuario);
            }

            return resultado;
        }

        public void Excluir(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _servUsuario.Excluir(comando);
                Validate(_servUsuario);
            }
        }
    }
}
