using System.Reflection;
using System.ComponentModel;
using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Aplicacao.Intreceptadores;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Comandos.TesteCmds;

namespace TemplateApi.Aplicacao
{
    public class TesteApp : Comum.BaseAplicacao
    {
        public TesteApp(
               AutenticacaoServ servAutenticacao,
               TesteServ servTeste,
               TesteInter interTeste)
               : base(servAutenticacao)
        {
            _servTeste = servTeste;
            _interTeste = interTeste;
        }

        protected readonly TesteServ _servTeste;
        protected readonly TesteInter _interTeste;

        /// <summary>
        /// Permite testar o recebimento de vários pormatos de dados.
        /// </summary>
        [AcessoLivre]
        [Display(Name = "Testar formatos de dados")]
        [Description("Permite testar o recebimento de vários pormatos de dados.")]
        public FormatosTesteCmd Formatos(FormatosTesteCmd comando)
        {
            Notifications.Clear();
            FormatosTesteCmd resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interTeste.Formatos(comando);
                resultado = _servTeste.Formatos(comando);
                IsValid(_servTeste);
            }

            return resultado;
        }
    }
}
