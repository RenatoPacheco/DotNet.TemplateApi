using System.Net;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DotNetCore.API.Template.Site.ValuesObject;

namespace DotNetCore.API.Template.Site.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!(context?.Exception is null))
            {
                ValidationNotification notificacao = new ValidationNotification();
                notificacao.AddFatal(context.Exception);
                HttpStatusCode codigo = HttpStatusCode.InternalServerError;
                Avisos avisos = new Avisos((int)codigo, notificacao);
                string dados = null;

                context.Result = new ObjectResult(new { 
                    avisos, dados
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
