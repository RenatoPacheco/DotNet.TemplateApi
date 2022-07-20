using System.Reflection;
using TemplateApi.Dominio.Servicos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Comandos.AutenticacaoCmds;
using TemplateApi.Aplicacao.Intreceptadores;

namespace TemplateApi.Aplicacao
{
    public class AutenticacaoApp : Comum.BaseAplicacao
    {
        public AutenticacaoApp(
            AutenticacaoServ servAutenticacao,
            AutenticacaoInter interAutenticacao)
            : base(servAutenticacao)
        {
            _interAutenticacao = interAutenticacao;
        }

        protected readonly AutenticacaoInter _interAutenticacao;

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

        /// <summary>
        /// Permite iniciar a autenticação pelo token e a chave pública.
        /// </summary>
        [NaoRequerAutorizacao, NaoRequerChavePublica]
        [Display(Name = "Iniciar autenticação")]
        [Description("Permite iniciar a autenticação pelo token e a chave pública.")]
        public Autenticacao Iniciar(IniciarAutenticacaoCmd comando)
        {
            Notifications.Clear();
            Autenticacao resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interAutenticacao.Iniciar(comando);
                resultado = _servAutenticacao.Iniciar(comando);
                Validate(_servAutenticacao);
            }

            return resultado;
        }
    }
}
