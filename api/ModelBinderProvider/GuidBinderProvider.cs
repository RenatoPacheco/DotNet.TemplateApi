using System;
using DotNetCore.API.Template.Site.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DotNetCore.API.Template.Site.ModelBinderProvider
{
    public class GuidBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(Guid) || context.Metadata.ModelType == typeof(Guid?))
            {
                return new BinderTypeModelBinder(typeof(GuidModelBinder));
            }

            return null;
        }
    }
}
