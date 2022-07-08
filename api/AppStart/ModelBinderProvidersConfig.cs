using Microsoft.AspNetCore.Mvc;
using DotNetCore.API.Template.Site.ModelBinderProvider;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Site
{
    public static class ModelBinderProvidersConfig
    {
        public static void Config(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new GuidBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntBinderProvider());
            options.ModelBinderProviders.Insert(0, new DateTimeBinderProvider());
            options.ModelBinderProviders.Insert(0, new EnumBinderProvider<Status>());
            options.ModelBinderProviders.Insert(0, new PhoneTypeBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntInputDataBinderProvider());
            options.ModelBinderProviders.Insert(0, new LongInputDataBinderProvider());
        }
    }
}
