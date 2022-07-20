using System;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Api.Settings.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.Settings.ModelBinderProvider
{
    public class BoolInputBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(BoolInput) || context.Metadata.ModelType == typeof(BoolInput?))
            {
                return new BinderTypeModelBinder(typeof(BoolInputModelBinder));
            }

            return null;
        }
    }
}
