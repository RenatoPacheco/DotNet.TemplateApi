using System;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Aplicacao
{
    public class AutorizacaoApp : Comum.BaseApp
    {
        public AutorizacaoApp(
            AutorizacaoServ servAutorizacao)
        {
            _servAutorizacao = servAutorizacao;
        }

        protected readonly AutorizacaoServ _servAutorizacao;

        /// <summary>
        /// Permite listar todas as autorizações existentes.
        /// </summary>
        [AcessoLivre]
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
