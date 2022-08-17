using System;
using TemplateApi.Api.App_Start.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class EnumBinderProvider<T> : IModelBinderProvider
        where T : struct
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(T) || context.Metadata.ModelType == typeof(T?))
            {
                return new BinderTypeModelBinder(typeof(EnumModelBinder<T>));
            }

            return null;
        }
    }
}
