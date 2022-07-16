using AutoMapper;
using DotNetCore.API.Template.Site.Settings.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.API.Template.Site
{
    public static class AutoMapperConfig
    {
        public static void Config(IServiceCollection services)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<ConteudoProfile>();
                cfg.AddProfile<StorageProfile>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
