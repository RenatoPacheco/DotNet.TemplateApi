using Microsoft.AspNetCore.Mvc;
using DotNetCore.API.Template.Site.Filters;

namespace DotNetCore.API.Template.Site
{
    public static class FiltersConfig
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
