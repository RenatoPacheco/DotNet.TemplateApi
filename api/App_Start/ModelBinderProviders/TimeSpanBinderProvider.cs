using System;
using TemplateApi.Api.App_Start.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class TimeSpanBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(TimeSpan) 
                || context.Metadata.ModelType == typeof(TimeSpan?)
                || context.Metadata.ModelType == typeof(TimeSpanInput))
            {
                return new BinderTypeModelBinder(typeof(TimeSpanModelBinder));
            }

            return null;
        }
    }
}
