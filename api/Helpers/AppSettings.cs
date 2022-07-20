using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace TemplateApi.Api.Helpers
{
    public class AppSettings : IAppSettings
    {
        public static IConfiguration Configuration { get; set; }

        private static string Renomear(string key)
        {
            if (key.ToLower().IndexOf("{ambiente}") > 0)
            {
                return Regex.Replace(key, @"{ambiente}",
                    DetectarServidor.Servidor, RegexOptions.IgnoreCase);
            }

            return key;
        }

        public string GetConnectionString(string keys)
        {
            return Configuration.GetConnectionString(Renomear(keys));
        }

        public T GetValue<T>(string keys)
        {
            return Configuration.GetValue<T>(Renomear(keys));
        }
    }
}
