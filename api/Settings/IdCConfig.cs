using DotNetCore.API.Template.IdC;
using DotNetCore.API.Template.Site.Helpers;
using DotNetCore.API.Template.Site.ApiApplications;
using Microsoft.Extensions.DependencyInjection;
using DotNetCore.API.Template.Site.ApiServices;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetCore.API.Template.Site
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
