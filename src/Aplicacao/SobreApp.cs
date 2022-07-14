using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Aplicacao.Intreceptadores;

namespace DotNetCore.API.Template.Aplicacao
{
    public class SobreApp : Comum.BaseApp
    {
        public SobreApp(
            AutenticacaoServ servAutenticacao,
            SobreServ servSobre,
            SobreInter interSobre)
            : base(servAutenticacao)
        {
            _servSobre = servSobre;
            _interSobre = interSobre;
        }

        protected readonly SobreServ _servSobre;
        protected readonly SobreInter _interSobre;

        /// <summary>
        /// Permite obter alguns dados sobre a aplicação.
        /// </summary>
        [AcessoLivre]
        [Display(Name = "Obter dados da aplicação")]
        [Description("Permite obter alguns dados sobre a aplicação.")]
        public Sobre Obter()
        {
            Notifications.Clear();
            Sobre resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servSobre.Obter();
                Validate(_servSobre);
            }

            return resultado;
        }
    }
}
