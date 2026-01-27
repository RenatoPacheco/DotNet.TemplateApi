using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace TemplateApi.Api {
    public static class CultureInfoConfig {
        public static void Config(IApplicationBuilder app) {
            CultureInfo cultureInfo = new(AppSettings.CultureInfo);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRequestLocalization(new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = new List<CultureInfo> { cultureInfo },
                SupportedUICultures = new List<CultureInfo> { cultureInfo }
            });
        }
    }
}
