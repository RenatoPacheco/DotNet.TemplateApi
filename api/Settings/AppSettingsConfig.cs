using Microsoft.Extensions.Configuration;

namespace TemplateApi.Api
{
    public static class AppSettingsConfig
    {
        public static void Config(IConfiguration configuration)
        {
            Helpers.AppSettings.Configuration = configuration;
            AppSettings.Inicializar(new Helpers.AppSettings());
            ConnectionStrings.Inicializar(new Helpers.AppSettings());
        }
    }
}
