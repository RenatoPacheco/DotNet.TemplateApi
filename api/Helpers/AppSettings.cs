using Microsoft.Extensions.Configuration;

namespace TemplateApi.Api.Helpers
{
    public class AppSettings : IAppSettings
    {
        public static IConfiguration Configuration { get; set; }

        public string GetConnectionString(string keys)
        {
            return Configuration.GetConnectionString(keys);
        }

        public T GetValue<T>(string keys)
        {
            return Configuration.GetValue<T>(keys);
        }
    }
}
