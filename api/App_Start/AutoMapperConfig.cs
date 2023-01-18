using AutoMapper;
using TemplateApi.Api.App_Start.AutoMappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TemplateApi.Api
{
    public static class AutoMapperConfig
    {
        public static void Config(IServiceCollection services)
        {
            services.AddSingleton(provider =>
            {
                MapperConfiguration config = new MapperConfiguration(cfg =>
                {
                    IServiceProvider invock = provider.CreateScope().ServiceProvider;
                    string basNnamespace = typeof(ConteudoProfile).Namespace;
                    Type[] listType = typeof(ConteudoProfile)
                        .Assembly.GetTypes().Where(
                        x => x.ReflectedType is null
                            && !(x.Namespace is null)
                            && x.Namespace == basNnamespace
                            && x.IsClass
                            && !x.IsAbstract
                            && !x.IsInterface).ToArray();

                    foreach (Type item in listType)
                    {
                        cfg.AddProfile(invock.GetService(item) as Profile);
                    }
                });

                config.AssertConfigurationIsValid();

                return config.CreateMapper();
            });
        }

    }
}
