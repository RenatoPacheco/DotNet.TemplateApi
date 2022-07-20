using System;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Api.Settings.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.Settings.ModelBinderProvider
{
    public class DoubleInputBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DoubleInput) || context.Metadata.ModelType == typeof(DoubleInput?))
            {
                return new BinderTypeModelBinder(typeof(DoubleInputModelBinder));
            }

            return null;
        }
    }
}
