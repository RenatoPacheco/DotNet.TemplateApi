using System;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Dominio.Servicos
{
    public class UsuarioServ : Comum.BaseServico
    {
        public UsuarioServ(
            IUsuarioRep repUsuario)
        {
            _repUsuario = repUsuario;
        }

        protected readonly IUsuarioRep _repUsuario;

        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Usuario> resultado = new ResultadoBusca<Usuario>();

            if (IsValid(comando))
            {
                resultado = _repUsuario.Filtrar(comando);
                IsValid(_repUsuario);
            }

            return resultado;
        }

        public Usuario Inserir(InserirUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (IsValid(comando))
            {
                comando.Aplicar(ref resultado);
                _repUsuario.Inserir(resultado);
                IsValid(_repUsuario);
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public Usuario Editar(EditarUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (IsValid(comando))
            {
                resultado = (Usuario)_repUsuario.Filtrar(new FiltrarUsuarioCmd {
                    Usuario = new int[] { comando.Usuario.Value },
                    Maximo = 1, Pagina = 1
                }, nameof(comando.Usuario), ValidationType.Error);
                IsValid(_repUsuario);

                if (IsValid())
                {
                    comando.Aplicar(ref resultado);
                    _repUsuario.Editar(resultado);
                    IsValid(_repUsuario);
                }
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public void Excluir(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();

            if (IsValid(comando))
            {
                _repUsuario.Excluir(comando);
                IsValid(_repUsuario);
            }
        }
    }
}
