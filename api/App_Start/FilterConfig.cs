using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.Filters;

namespace TemplateApi.Api
{
    public static class FilterConfig
    {
        public static void Config(MvcOptions options)
        {
            // Incluindo filtro para customizar erro 500 da API
            options.Filters.Add<HttpResponseExceptionFilter>();
            // Inicializa o acesso a aplicação
            options.Filters.Add<InicializarAcessoFilter>();
            // Bloqueando acesso a todas as rotas com base na autorização de acesso
            options.Filters.Add<ValidarAutorizacaoFilter>();
        }
    }
}
