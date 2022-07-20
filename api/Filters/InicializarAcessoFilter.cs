using Microsoft.AspNetCore.Mvc.Filters;
using TemplateApi.Api.ApiApplications;

namespace TemplateApi.Api.Filters
{
    public class InicializarAcessoFilter
        : IAuthorizationFilter, IOrderedFilter
    {
        public InicializarAcessoFilter(
            AutenticacaoApiApp autenticacaoApiServ)
        {
            _autenticacaoApiServ = autenticacaoApiServ;
        }

        protected readonly AutenticacaoApiApp _autenticacaoApiServ;

        public int Order { get; set; } = int.MaxValue - 99;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _autenticacaoApiServ.Iniciar();
        }
    }
}
