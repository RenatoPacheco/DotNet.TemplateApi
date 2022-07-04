using Microsoft.AspNetCore.Mvc;
using DotNetCore.API.Template.Site.ModelBinderProvider;

namespace DotNetCore.API.Template.Site
{
    public static class ModelBinderProvidersConfig
    {
        public static void Config(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new GuidBinderProvider());
            options.ModelBinderProviders.Insert(0, new IntBinderProvider());
            options.ModelBinderProviders.Insert(0, new DateTimeBinderProvider());
        }
    }
}
