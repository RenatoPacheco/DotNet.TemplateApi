using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DotNetCore.API.Template.Compartilhado.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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

            services.AddHttpContextAccessor();

            services.TryAddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, DefaultApiDescriptionProvider>());

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

            SwaggerConfig.Config(services);
            AutoMapperConfig.Config(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfoConfig.Config(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SwaggerConfig.Config(app);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
