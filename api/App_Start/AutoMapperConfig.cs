using AutoMapper;
using TemplateApi.Api.App_Start.AutoMappers;
using Microsoft.Extensions.DependencyInjection;
using TemplateApi.Api.ApiServices;

namespace TemplateApi.Api
{
    public static class AutoMapperConfig
    {
        public static void Config(IServiceCollection services)
        {
            services.AddSingleton(provider => {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ConteudoProfile());
                    cfg.AddProfile(new CustonTypesProfile());
                    cfg.AddProfile(new StorageProfile(
                        provider.CreateScope().ServiceProvider.GetService<RequestApiServ>()));
                    cfg.AddProfile(new TesteProfile());
                    cfg.AddProfile(new UploadProfile(
                        provider.CreateScope().ServiceProvider.GetService<RequestApiServ>()));
                    cfg.AddProfile(new UsuarioProfile());
                });

                config.AssertConfigurationIsValid();

                return config.CreateMapper();
            });
        }
    }
}
