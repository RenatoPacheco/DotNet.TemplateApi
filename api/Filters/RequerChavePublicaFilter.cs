using System.Linq;
using System.Net;
using System.Reflection;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Recurso;
using DotNetCore.API.Template.Site.ValuesObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetCore.API.Template.Site.Filters
{
    public class RequerChavePublicaFilter : IAuthorizationFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ControllerActionDescriptor action = context.ActionDescriptor as ControllerActionDescriptor;

            bool acessoLivre = action.MethodInfo.GetCustomAttributes<AcessoLivreAttribute>().Any()
                || action.ControllerTypeInfo.GetCustomAttributes<AcessoLivreAttribute>().Any();

            if (!acessoLivre)
            {
                IHeaderDictionary headers = context.HttpContext.Request.Headers;
                string chavePublica = string.Empty;
                string compare = AppSettings.ChavePublica;

                if (headers.ContainsKey("Chave-Publica"))
                    chavePublica = headers["Chave-Publica"].ToString();

                if (string.IsNullOrWhiteSpace(chavePublica) || chavePublica != compare)
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
