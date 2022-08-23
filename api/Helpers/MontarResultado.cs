using System.Net;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Compartilhado.Json;
using Newtonsoft.Json;

namespace TemplateApi.Api.Helpers
{
    public static class MontarResultado
    {
        public static JsonResult Json(HttpStatusCode codigo, ValidationNotification notificacoes)
        {
            Avisos avisos = new Avisos((int)codigo, notificacoes);

            return new JsonResult(new ComumViewData
            { 
                Avisos = avisos
            }, ConfiguracaoJson.AplicarParaEscrita(new JsonSerializerSettings()));
        }

        public static JsonResult Json(HttpStatusCode codigo, ValidationNotification notificacoes, object dados)
        {
            Avisos avisos = new Avisos((int)codigo, notificacoes);

            return new JsonResult(new ComumViewData<object>
            {
                Avisos = avisos,
                Dados = dados
            }, ConfiguracaoJson.AplicarParaEscrita(new JsonSerializerSettings()));
        }
    }
}
