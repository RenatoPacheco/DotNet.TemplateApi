using Microsoft.AspNetCore.Mvc.Filters;
using DotNetCore.API.Template.Site.ApiApplications;

namespace DotNetCore.API.Template.Site.Filters
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
