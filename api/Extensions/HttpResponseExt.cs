using System.Net;
using BitHelp.Core.Validation;
using TemplateApi.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TemplateApi.Compartilhado.Json;

namespace TemplateApi.Api.Extensions
{
    public static class HttpResponseExt
    {
        public static JsonResult ToJson(this HttpResponse source, HttpStatusCode codigo, ValidationNotification notificacoes)
        {
            source.StatusCode = (int)codigo;
            return MontarResultado.Json(codigo, notificacoes);
        }

        public static JsonResult ToJson(this HttpResponse source, HttpStatusCode codigo, ValidationNotification notificacoes, object dados)
        {
            source.StatusCode = (int)codigo;
            return MontarResultado.Json(codigo, notificacoes, dados);
        }

        public static JsonResult ToJson(this HttpResponse source, HttpStatusCode codigo, object dados)
        {
            source.StatusCode = (int)codigo;
            return new JsonResult(dados, ConfiguracaoJson.Escrita());
        }
    }
}
