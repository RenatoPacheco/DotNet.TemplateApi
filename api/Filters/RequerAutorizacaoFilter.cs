using System.Net;
using System.Linq;
using System.Reflection;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DotNetCore.API.Template.Recurso;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using DotNetCore.API.Template.Site.ValuesObject;

namespace DotNetCore.API.Template.Site.Filters
{
    public class RequerAutorizacaoFilter
        : IAuthorizationFilter, IOrderedFilter
    {

        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ControllerActionDescriptor action = context.ActionDescriptor as ControllerActionDescriptor;

            bool acessoLivre = action.MethodInfo.GetCustomAttributes<AcessoLivreAttribute>().Any()
                || action.ControllerTypeInfo.GetCustomAttributes<AcessoLivreAttribute>().Any()
                || action.MethodInfo.GetCustomAttributes<NaoRequerAutorizazaoAttribute>().Any()
                || action.ControllerTypeInfo.GetCustomAttributes<NaoRequerAutorizazaoAttribute>().Any();

            if (!acessoLivre)
            {
                IHeaderDictionary headers = context.HttpContext.Request.Headers;
                string autorizacao = string.Empty;
                string compare = AppSettings.Autorizacao;

                if (headers.ContainsKey("Authorization"))
                {
                    autorizacao = headers["Authorization"].ToString();
                    autorizacao = autorizacao.StartsWith("Bearer ") ? autorizacao.Substring(7) : autorizacao;
                }

                if (string.IsNullOrWhiteSpace(autorizacao) || autorizacao != compare)
                {
                    ValidationNotification notificacao = new ValidationNotification();
                    notificacao.AddError(AvisosResx.AcessoNaoAutorizado);
                    HttpStatusCode codigo = HttpStatusCode.Unauthorized;
                    Notificacao avisos = new Notificacao((int)codigo, notificacao);
                    string dados = null;

                    context.HttpContext.Response.StatusCode = (int)codigo;
                    context.Result = new ObjectResult(new
                    {
                        avisos,
                        dados
                    });
                }
            }
        }
    }
}
