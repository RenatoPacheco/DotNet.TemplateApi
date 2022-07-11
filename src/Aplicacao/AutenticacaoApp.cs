using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Dominio.Entidades;

namespace DotNetCore.API.Template.Aplicacao
{
    public class AutenticacaoApp : Comum.BaseApp
    {
        public AutenticacaoApp(
            AutenticacaoServ servAutenticacao)
        {
            _servAutenticacao = servAutenticacao;
        }

        protected readonly AutenticacaoServ _servAutenticacao;

        /// <summary>
        /// Permite listar os dados da autenticação atual.
        /// </summary>
        [AcessoLivre]
        [Display(Name = "Obter dados da autenticação atual")]
        [Description("Permite listar os dados da autenticação atual.")]
        public Autenticacao Obter()
        {
            Notifications.Clear();
            Autenticacao resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servAutenticacao.Obter();
                Validate(_servAutenticacao);
            }

            return resultado;
        }
    }
}
