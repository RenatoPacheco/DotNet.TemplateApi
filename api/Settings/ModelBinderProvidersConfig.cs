using Microsoft.AspNetCore.Mvc;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Site.Settings.ModelBinderProvider;

namespace DotNetCore.API.Template.Site
{
    public static class ModelBinderProvidersConfig
    {
        public static void Config(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new GuidBinderProvider());
            options.ModelBinderProviders.Insert(0, new BoolBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntBinderProvider());
            options.ModelBinderProviders.Insert(0, new DateTimeBinderProvider());
            options.ModelBinderProviders.Insert(0, new EnumBinderProvider<Status>());
            options.ModelBinderProviders.Insert(0, new PhoneTypeBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new LongInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new BoolInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new GuidInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new EnumInputBinderProvider<Status>());
        }
    }
}
