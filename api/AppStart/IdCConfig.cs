using DotNetCore.API.Template.IdC;
using DotNetCore.API.Template.Site.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.API.Template.Site
{
    public static class IdCConfig
    {
        public static void Config(IServiceCollection services)
        {
            ResolverDependencia dependencia = new ResolverDependencia(services);
            Injecao.Carregar(dependencia);
        }
    }
}
