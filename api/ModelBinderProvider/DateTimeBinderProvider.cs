using System;
using DotNetCore.API.Template.Site.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DotNetCore.API.Template.Site.ModelBinderProvider
{
    public class DateTimeBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
            {
                return new BinderTypeModelBinder(typeof(DateTimeModelBinder));
            }

            return null;
        }
    }
}
