using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Aplicacao.Interceptadores;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Aplicacao
{
    public class UsuarioApp : Comum.BaseAplicacao
    {
        public UsuarioApp(
            AutenticacaoServ servAutenticacao,
            UsuarioServ servUsuario,
            UsuarioInter interUsuario)
            : base(servAutenticacao)
        {
            _servUsuario = servUsuario;
            _interUsuario = interUsuario;
        }

        protected readonly UsuarioServ _servUsuario;
        protected readonly UsuarioInter _interUsuario;

        /// <summary>
        /// Permite filtrar os usuários.
        /// </summary>
        [Display(Name = "Filtrar usuário")]
        [Description("Permite filtrar os usuários.")]
        public ResultadoBusca<Usuario> Filtrar(FiltrarUsuarioCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Usuario> resultado = new ResultadoBusca<Usuario>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interUsuario.Filtrar(comando);
                resultado = _servUsuario.Filtrar(comando);
                IsValid(_servUsuario);
            }

            return resultado;
        }

        /// <summary>
        /// Permite inserir um novo usuário.
        /// </summary>
        [Display(Name = "Inserir usuário")]
        [Description("Permite inserir um novo usuário.")]
        public Usuario Inserir(InserirUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interUsuario.Inserir(comando);
                resultado = _servUsuario.Inserir(comando);
                IsValid(_servUsuario);
            }

            return resultado;
        }

        /// <summary>
        /// Permite editar um usuário específico.
        /// </summary>
        [Display(Name = "Editar usuário")]
        [Description("Permite editar um usuário específico.")]
        public Usuario Editar(EditarUsuarioCmd comando)
        {
            Notifications.Clear();
            Usuario resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interUsuario.Editar(comando);
                resultado = _servUsuario.Editar(comando);
                IsValid(_servUsuario);
            }

            return resultado;
        }

        /// <summary>
        /// Permite excluir um ou mais usuários.
        /// </summary>
        [Display(Name = "Excluir usuário")]
        [Description("Permite excluir um ou mais usuários.")]
        public void Excluir(ExcluirUsuarioCmd comando)
        {
            Notifications.Clear();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interUsuario.Excluir(comando);
                _servUsuario.Excluir(comando);
                IsValid(_servUsuario);
            }
        }
    }
}
