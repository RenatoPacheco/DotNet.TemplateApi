using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class IntBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(int) 
                || context.Metadata.ModelType == typeof(int?)
                || context.Metadata.ModelType == typeof(IntInput))
            {
                return new BinderTypeModelBinder(typeof(ModelBinders.IntModelBinder));
            }

            return null;
        }
    }
}
