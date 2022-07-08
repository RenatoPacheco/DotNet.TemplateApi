using System;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;
using DotNetCore.API.Template.Site.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DotNetCore.API.Template.Site.ModelBinderProvider
{
    public class IntInputBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(IntInput) || context.Metadata.ModelType == typeof(IntInput?))
            {
                return new BinderTypeModelBinder(typeof(IntInputModelBinder));
            }

            return null;
        }
    }
}
