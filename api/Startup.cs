using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotNetCore.API.Template.Dominio.Auxiliares;

namespace DotNetCore.API.Template.Site
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
            CorsConfig.Config(services);
            IdCConfig.Config(services);

            services.AddControllers(options => {
                // Aplicando filtrdo customizados
                FiltersConfig.Config(options);
                // Aplicando binders customizados
                ModelBinderProvidersConfig.Config(options);
            }).ConfigureApiBehaviorOptions(options => {
                // Desabilitando o filtro que intecepta erros do ModelState
                options.SuppressModelStateInvalidFilter = true;
            }).AddJsonOptions(options => {
                ContratoJson.Configurar(options.JsonSerializerOptions);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo cultureInfo = new CultureInfo(AppSettings.CultureInfo);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
