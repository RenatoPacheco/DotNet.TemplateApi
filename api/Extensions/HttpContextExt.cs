using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TemplateApi.Api.Extensions
{
    public static class HttpContextExt
    {
        public static void RedirectToErrorPage(this HttpContext source)
        {
            source.Response.Redirect($"/html/erro/{source.Response.StatusCode}?path={source.Request.Path}");
        }
    }
}
