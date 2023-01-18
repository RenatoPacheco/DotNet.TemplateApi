using AutoMapper;
using TemplateApi.Api.App_Start.AutoMappers;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateApi.Api
{
    public static class AutoMapperConfig
    {
        public static void Config(IServiceCollection services)
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustonTypesProfile));
            });

            config.AssertConfigurationIsValid();

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
