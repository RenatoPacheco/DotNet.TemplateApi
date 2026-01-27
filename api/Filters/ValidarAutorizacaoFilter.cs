using System.Net;
using TemplateApi.Recursos;
using BitHelp.Core.Validation;
using TemplateApi.Api.Helpers;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.ApiApplications;
using Microsoft.AspNetCore.Mvc.Filters;
using TemplateApi.Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace TemplateApi.Api.Filters {
    public class ValidarAutorizacaoFilter
        : IAuthorizationFilter, IOrderedFilter {

        public ValidarAutorizacaoFilter(
            AutenticacaoApiApp autenticacaoApiServ) {
            _autenticacaoApiServ = autenticacaoApiServ;
        }

        protected readonly AutenticacaoApiApp _autenticacaoApiServ;

        public int Order { get; set; } = int.MaxValue - 100;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnAuthorization(AuthorizationFilterContext context) {
            ControllerActionDescriptor action = context.ActionDescriptor as ControllerActionDescriptor;

            bool ignorarFiltro = action.MethodInfo.GetCustomAttributes(
                typeof(IgnorarFiltroAutorizacaoAttribute), true).Any();

            if (!ignorarFiltro && !_autenticacaoApiServ.EstaAutorizado(action)) {
                Autorizacao requisito = _autenticacaoApiServ.ExtrairAutorizacao(action);
                ValidationNotification notificacao = new();

                if (!_autenticacaoApiServ.HaChavePublica() && requisito.RequerChavePublica) {
                    notificacao.AddError(AvisosResx.ChavePublicaNaoRecebiada);
                }

                if (!_autenticacaoApiServ.HaToken() && requisito.RequerAutorizacao) {
                    notificacao.AddError(AvisosResx.TokenDeAutenticacaoNaoRecebido);
                }

                if (notificacao.IsValid()) {
                    notificacao.AddError(AvisosResx.AcessoNaoAutorizado);
                }

                HttpStatusCode codigo = HttpStatusCode.Unauthorized;
                context.HttpContext.Response.StatusCode = (int)codigo;

                if (action.ControllerTypeInfo.IsApi()) {
                    context.Result = MontarResultado.Json(codigo, notificacao);
                } else {
                    context.HttpContext.RedirectToErrorPage();
                }

            }
        }
    }
}
