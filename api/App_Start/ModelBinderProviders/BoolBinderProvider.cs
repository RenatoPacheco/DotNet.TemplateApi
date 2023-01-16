using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class BoolBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(bool) 
                || context.Metadata.ModelType == typeof(bool?)
                || context.Metadata.ModelType == typeof(BoolInput))
            {
                return new BinderTypeModelBinder(typeof(ModelBinders.BoolModelBinder));
            }

            return null;
        }
    }
}
