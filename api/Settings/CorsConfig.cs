using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.API.Template.Site
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
