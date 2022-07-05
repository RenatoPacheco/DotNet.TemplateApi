using System.Net;
using System.Text;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Dominio.Auxiliares;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore.API.Template.Site.Helpers
{
    public static class MontarResultado
    {
        public static JsonResult Json(HttpStatusCode codigo, ValidationNotification notificacoes)
        {
            Notificacao avisos = new Notificacao((int)codigo, notificacoes);
            object dados = null;

            return new JsonResult(new 
            { 
                Avisos = avisos,
                Dados = dados
            }, ContratoJson.Configuracao);
        }

        public static JsonResult Json(HttpStatusCode codigo, ValidationNotification notificacoes, object dados)
        {
            Notificacao avisos = new Notificacao((int)codigo, notificacoes);
            StringBuilder resultado = new StringBuilder();

            return new JsonResult(new
            {
                Avisos = avisos,
                Dados = dados
            }, ContratoJson.Configuracao);
        }
    }
}
