using System;
using BitHelp.Core.Type.pt_BR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DotNetCore.API.Template.Site.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace DotNetCore.API.Template.Site.ModelBinderProvider
{
    public class PhoneTypeBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(PhoneType) || context.Metadata.ModelType == typeof(PhoneType?))
            {
                return new BinderTypeModelBinder(typeof(PhoneTypeModelBinder));
            }

            return null;
        }
    }
}
