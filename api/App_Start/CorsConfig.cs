using Microsoft.Extensions.DependencyInjection;

namespace TemplateApi.Api
{
    public static class CorsConfig
    {
        public static void Config(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("*");
                });
            });
        }
    }
}
