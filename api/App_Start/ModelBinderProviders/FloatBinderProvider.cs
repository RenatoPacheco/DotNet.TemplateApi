using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class FloatBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(float) 
                || context.Metadata.ModelType == typeof(float?)
                || context.Metadata.ModelType == typeof(FloatInput))
            {
                return new BinderTypeModelBinder(typeof(ModelBinders.FloatModelBinder));
            }

            return null;
        }
    }
}
