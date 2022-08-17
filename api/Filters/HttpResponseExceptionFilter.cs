using System.Net;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc.Filters;
using TemplateApi.Api.Helpers;

namespace TemplateApi.Api.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 98;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!(context?.Exception is null))
            {
                HttpStatusCode codigo = HttpStatusCode.InternalServerError;
                ValidationNotification notificacao = new ValidationNotification();
                notificacao.AddFatal(context.Exception);

                context.Result = MontarResultado.Json(codigo, notificacao);
                context.ExceptionHandled = true;
            }
        }
    }
}
