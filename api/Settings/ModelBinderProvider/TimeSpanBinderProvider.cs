using System;
using TemplateApi.Api.Settings.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.Settings.ModelBinderProvider
{
    public class TimeSpanBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(TimeSpan) || context.Metadata.ModelType == typeof(TimeSpan?))
            {
                return new BinderTypeModelBinder(typeof(TimeSpanModelBinder));
            }

            return null;
        }
    }
}
