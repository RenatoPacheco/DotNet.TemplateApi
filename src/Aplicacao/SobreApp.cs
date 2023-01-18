using System.Reflection;
using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Aplicacao.Interceptadores;

namespace TemplateApi.Aplicacao
{
    public class SobreApp : Comum.BaseAplicacao
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
                IsValid(_servSobre);
            }

            return resultado;
        }
    }
}
