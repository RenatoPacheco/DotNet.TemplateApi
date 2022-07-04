using System;
using System.Linq;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class UsuarioServ : Comum.BaseServ
    {
        public UsuarioServ(
            IUsuarioRep repUsuario)
        {
            _repUsuario = repUsuario;
        }

        protected readonly IUsuarioRep _repUsuario;

        public Usuario[] Filtrar(FiltrarUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario[] resultado = Array.Empty<Usuario>();

            if (Validate(comando))
            {
                resultado = (Usuario[])_repUsuario.Filtrar(comando);
                Validate(_repUsuario);
            }

            return resultado;
        }

        public Usuario Inserir(InserirUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (Validate(comando))
            {
                comando.Aplicar(ref resultado);
                _repUsuario.Inserir(resultado);
                Validate(_repUsuario);
            }

            return resultado;
        }

        public Usuario Editar(EditarUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (Validate(comando))
            {
                resultado = (Usuario)_repUsuario.Filtrar(new FiltrarUsuarioCmd {
                    Usuario = new int[] { comando.Usuario.Value }
                });
                Validate(_repUsuario);

                if (IsValid())
                {
                    comando.Aplicar(ref resultado);
                    _repUsuario.Inserir(resultado);
                    Validate(_repUsuario);
                }
            }

            return resultado;
        }

        public void Excluir(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();

            if (Validate(comando))
            {
                _repUsuario.Excluir(comando);
                Validate(_repUsuario);
            }
        }
    }
}
