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
            // Bloqueando acesso a todas as rotas por padrão
            options.Filters.Add<RequerAutorizacaoFilter>();
            options.Filters.Add<RequerChavePublicaFilter>();

        }
    }
}
