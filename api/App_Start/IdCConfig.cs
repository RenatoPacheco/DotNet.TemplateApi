using TemplateApi.IdC;
using TemplateApi.Api.Helpers;
using TemplateApi.Api.ApiServices;
using TemplateApi.Api.ApiApplications;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TemplateApi.Api
{
    public static class IdCConfig
    {
        public static void Config(IServiceCollection services)
        {
            services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultApiDescriptionProvider>());

            Helpers.ResolverDependencias resolve = new ResolverDependencias(services);
            RegistrarDependencias.Carregar(resolve);

            resolve.Escopo<AutorizacaoApiServ>();
            resolve.Escopo<RequestApiServ>();

            resolve.Escopo<AutenticacaoApiApp>();
        }
    }
}
