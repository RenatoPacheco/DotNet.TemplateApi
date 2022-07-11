using System;
using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using DotNetCore.API.Template.Dominio.Notacoes;

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
        /// Lista todas as autorizações existentes
        /// </summary>
        [AcessoLivre]
        [Display(Name = "Listar autorizações")]
        [Description("Lista todas as autorizações existentes.")]
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
