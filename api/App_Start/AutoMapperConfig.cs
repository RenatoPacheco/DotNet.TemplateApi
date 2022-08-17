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
                cfg.AddProfile<CustonTypesProfile>();
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<ConteudoProfile>();
                cfg.AddProfile<StorageProfile>();
                cfg.AddProfile<TesteProfile>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
