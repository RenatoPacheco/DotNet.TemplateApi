using System;
using TemplateApi.Api.Settings.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.Settings.ModelBinderProvider
{
    public class DoubleBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(double) || context.Metadata.ModelType == typeof(double?))
            {
                return new BinderTypeModelBinder(typeof(IntModelBinder));
            }

            return null;
        }
    }
}
