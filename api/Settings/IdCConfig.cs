using TemplateApi.IdC;
using TemplateApi.Api.Helpers;
using TemplateApi.Api.ApiApplications;
using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Api.ApiServices;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TemplateApi.Api
{
    public static class IdCConfig
    {
        public static void Config(IServiceCollection services)
        {
            services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultApiDescriptionProvider>());

            ResolverDependencia dependencia = new ResolverDependencia(services);
            Injecao.Carregar(dependencia);

            dependencia.Escopo<AutorizacaoApiServ>();
            dependencia.Escopo<RequestApiServ>();

            dependencia.Escopo<AutenticacaoApiApp>();
        }
    }
}
