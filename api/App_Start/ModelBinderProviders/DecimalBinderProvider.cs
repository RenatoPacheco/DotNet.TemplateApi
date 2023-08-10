using TemplateApi.Api.App_Start.ModelBinders;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class DecimalBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(decimal)
                || context.Metadata.ModelType == typeof(decimal?)
                || context.Metadata.ModelType == typeof(DecimalInput))
            {
                return new BinderTypeModelBinder(typeof(IntModelBinder));
            }

            return null;
        }
    }
}
