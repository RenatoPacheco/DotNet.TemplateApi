using System.Net;
using BitHelp.Core.Validation;
using TemplateApi.Recurso;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using TemplateApi.Api.Helpers;
using TemplateApi.Api.ApiApplications;
using TemplateApi.Dominio.ObjetosDeValor;
using System.Linq;

namespace TemplateApi.Api.Filters
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

        public int Order { get; set; } = int.MaxValue - 100;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ControllerActionDescriptor action = context.ActionDescriptor as ControllerActionDescriptor;
            
            bool ignorarFiltro = action.MethodInfo.GetCustomAttributes(
                typeof(IgnorarFiltroAutorizacaoAttribute), true)
                .Select(x => x as IgnorarFiltroAutorizacaoAttribute).Any();

            if (!ignorarFiltro && !_autenticacaoApiServ.EstaAutorizado(action))
            {
                Autorizacao requisito = _autenticacaoApiServ.ExtrairAutorizacao(action);
                HttpStatusCode codigo = HttpStatusCode.Unauthorized;
                ValidationNotification notificacao = new ValidationNotification();
                
                if (!_autenticacaoApiServ.HaChavePublica() && requisito.RequerChavePublica)
                {
                    notificacao.AddError(AvisosResx.ChavePublicaNaoRecebiada);
                }
                if (!_autenticacaoApiServ.HaToken() && requisito.RequerAutorizacao)
                {
                    notificacao.AddError(AvisosResx.TokenDeAutenticacaoNaoRecebido);
                }
                if (notificacao.IsValid())
                {
                    notificacao.AddError(AvisosResx.AcessoNaoAutorizado);
                }

                context.HttpContext.Response.StatusCode = (int)codigo;
                context.Result = MontarResultado.Json(codigo, notificacao);
            }
        }
    }
}
