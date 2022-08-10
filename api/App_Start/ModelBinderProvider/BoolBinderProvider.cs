using System;
using TemplateApi.Api.App_Start.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.App_Start.ModelBinderProvider
{
    public class BoolBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(bool) || context.Metadata.ModelType == typeof(bool?))
            {
                return new BinderTypeModelBinder(typeof(BoolModelBinder));
            }

            return null;
        }
    }
}
