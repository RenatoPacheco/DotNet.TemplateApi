using System;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;
using DotNetCore.API.Template.Site.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DotNetCore.API.Template.Site.ModelBinderProvider
{
    public class GuidInputBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(GuidInput) || context.Metadata.ModelType == typeof(GuidInput?))
            {
                return new BinderTypeModelBinder(typeof(GuidInputModelBinder));
            }

            return null;
        }
    }
}
