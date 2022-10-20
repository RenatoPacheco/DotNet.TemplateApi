using TemplateApi.IdC;
using TemplateApi.Api.Helpers;
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

            ResolverDependencias resolve = new ResolverDependencias(services);

            RegistrarDependencias.Aplicar(resolve);
            RegistrarDependencias.Aplicar(resolve, new ModuloDependencias());
        }
    }
}
