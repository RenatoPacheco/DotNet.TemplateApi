using System;
using DotNetCore.API.Template.Site.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DotNetCore.API.Template.Site.ModelBinderProvider
{
    public class IntBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(int) || context.Metadata.ModelType == typeof(int?))
            {
                return new BinderTypeModelBinder(typeof(IntModelBinder));
            }

            return null;
        }
    }
}
