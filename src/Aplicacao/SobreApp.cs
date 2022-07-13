using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DotNetCore.API.Template.Dominio.Notacoes;

namespace DotNetCore.API.Template.Aplicacao
{
    public class SobreApp : Comum.BaseApp
    {
        public SobreApp(
            SobreServ servSobre)
        {
            _servSobre = servSobre;
        }

        protected readonly SobreServ _servSobre;

        /// <summary>
        /// Permite obter alguns dados sobre a aplicação.
        /// </summary>
        [NaoRequerAutorizacao, NaoRequerChavePublica]
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
