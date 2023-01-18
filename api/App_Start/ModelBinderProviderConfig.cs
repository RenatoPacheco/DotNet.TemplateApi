using Microsoft.AspNetCore.Mvc;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Api.App_Start.ModelBinderProviders;

namespace TemplateApi.Api
{
    public static class ModelBinderProviderConfig
    {
        public static void Config(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new GuidBinderProvider());
            options.ModelBinderProviders.Insert(0, new TimeSpanBinderProvider());
            options.ModelBinderProviders.Insert(0, new BoolBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntBinderProvider());
            options.ModelBinderProviders.Insert(0, new LongBinderProvider());
            options.ModelBinderProviders.Insert(0, new FloatBinderProvider());
            options.ModelBinderProviders.Insert(0, new DoubleBinderProvider());
            options.ModelBinderProviders.Insert(0, new DecimalBinderProvider());
            options.ModelBinderProviders.Insert(0, new DateTimeBinderProvider());
            options.ModelBinderProviders.Insert(0, new PhoneTypeBinderProvider());
            options.ModelBinderProviders.Insert(0, new EnumBinderProvider<Status>());
            options.ModelBinderProviders.Insert(0, new EnumBinderProvider<ContextoCmd>());
        }
    }
}
