using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TemplateApi.Compartilhado.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettingsConfig.Config(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            CorsConfig.Config(services);
            IdCConfig.Config(services);

            services.AddControllers(options => {
                // Aplicando filtrdo customizados
                FilterConfig.Config(options);
                // Aplicando binders customizados
                ModelBinderProviderConfig.Config(options);
            }).ConfigureApiBehaviorOptions(options => {
                // Desabilitando o filtro que intecepta erros do ModelState
                options.SuppressModelStateInvalidFilter = true;
            }).AddNewtonsoftJson(options => {
                // Customizando configuração para Json
                ConfiguracaoJson.Escrita(options.SerializerSettings);
            });

            SwashbuckleConfig.Config(services);
            AutoMapperConfig.Config(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppSettingsConfig.Config(env);
            CultureInfoConfig.Config(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SwashbuckleConfig.Config(app);
            }

            FileConfig.Config(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
