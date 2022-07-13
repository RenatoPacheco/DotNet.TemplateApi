using System;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Aplicacao.Intreceptadores;

namespace DotNetCore.API.Template.Aplicacao
{
    public class AutorizacaoApp : Comum.BaseApp
    {
        public AutorizacaoApp(
            AutorizacaoServ servAutorizacao,
            AutorizacaoInter interAutorizacao)
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
