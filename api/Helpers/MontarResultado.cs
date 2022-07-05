using System.Net;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Dominio.Auxiliares;

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

            return new JsonResult(new
            {
                Avisos = avisos,
                Dados = dados
            }, ContratoJson.Configuracao);
        }
    }
}
