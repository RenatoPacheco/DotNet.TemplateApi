using System;
using BitHelp.Core.Type.pt_BR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Api.App_Start.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
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
