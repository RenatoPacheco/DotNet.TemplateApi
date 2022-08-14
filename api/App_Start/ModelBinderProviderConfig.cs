using Microsoft.AspNetCore.Mvc;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Api.App_Start.ModelBinderProviders;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Api
{
    public static class ModelBinderProviderConfig
    {
        public static void Config(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new GuidBinderProvider());
            options.ModelBinderProviders.Insert(0, new GuidInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new TimeSpanBinderProvider());
            options.ModelBinderProviders.Insert(0, new TimeSpanInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new BoolBinderProvider());
            options.ModelBinderProviders.Insert(0, new BoolInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new LongBinderProvider());
            options.ModelBinderProviders.Insert(0, new LongInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new DoubleBinderProvider());
            options.ModelBinderProviders.Insert(0, new DoubleInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new DecimalBinderProvider());
            options.ModelBinderProviders.Insert(0, new DecimalInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new DateTimeBinderProvider());
            options.ModelBinderProviders.Insert(0, new DateTimeInputBinderProvider());
            options.ModelBinderProviders.Insert(0, new PhoneTypeBinderProvider());
            options.ModelBinderProviders.Insert(0, new EnumBinderProvider<Status>());
            options.ModelBinderProviders.Insert(0, new EnumInputBinderProvider<Status>());
            options.ModelBinderProviders.Insert(0, new EnumBinderProvider<ContextoCmd>());
            options.ModelBinderProviders.Insert(0, new EnumInputBinderProvider<ContextoCmd>());
        }
    }
}
