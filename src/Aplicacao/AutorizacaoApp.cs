using System;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Aplicacao.Intreceptadores;

namespace TemplateApi.Aplicacao
{
    public class AutorizacaoApp : Comum.BaseAplicacao
    {
        public AutorizacaoApp(
            AutenticacaoServ servAutenticacao,
            AutorizacaoServ servAutorizacao,
            AutorizacaoInter interAutorizacao)
            : base(servAutenticacao)
        {
            _servAutorizacao = servAutorizacao;
            _interAutorizacao = interAutorizacao;
        }

        protected readonly AutorizacaoServ _servAutorizacao;
        protected readonly AutorizacaoInter _interAutorizacao;

        /// <summary>
        /// Permite listar todas as autorizações existentes.
        /// </summary>
        [NaoRequerAutorizacao, NaoRequerChavePublica]
        [Display(Name = "Listar autorizações")]
        [Description("Permite listar todas as autorizações existentes.")]
        public Autorizacao[] Listar()
        {
            Notifications.Clear();
            Autorizacao[] resultado = Array.Empty<Autorizacao>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servAutorizacao.Listar();
                Validate(_servAutorizacao);
            }

            return resultado;
        }
    }
}
