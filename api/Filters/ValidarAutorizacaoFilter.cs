using System.Net;
using BitHelp.Core.Validation;
using DotNetCore.API.Template.Recurso;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using DotNetCore.API.Template.Site.Helpers;
using DotNetCore.API.Template.Site.ApiApplications;

namespace DotNetCore.API.Template.Site.Filters
{
    public class ValidarAutorizacaoFilter
        : IAuthorizationFilter, IOrderedFilter
    {

        public ValidarAutorizacaoFilter(
            AutenticacaoApiApp autenticacaoApiServ)
        {
            _autenticacaoApiServ = autenticacaoApiServ;
        }

        protected readonly AutenticacaoApiApp _autenticacaoApiServ;

        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ControllerActionDescriptor action = context.ActionDescriptor as ControllerActionDescriptor;

            _autenticacaoApiServ.Iniciar();

            if (!_autenticacaoApiServ.EstaAutorizado(action))
            {
                string mensagem = !_autenticacaoApiServ.HaChavePublica()
                    ? AvisosResx.ChavePublicaNaoRecebiada
                    : !_autenticacaoApiServ.HaToken()
                    ? AvisosResx.TokenDeAutenticacaoNaoRecebido
                    : AvisosResx.AcessoNaoAutorizado;

                ValidationNotification notificacao = new ValidationNotification();
                notificacao.AddError(mensagem);
                HttpStatusCode codigo = HttpStatusCode.Unauthorized;

                context.HttpContext.Response.StatusCode = (int)codigo;
                context.Result = MontarResultado.Json(codigo, notificacao);
            }
        }
    }
}
