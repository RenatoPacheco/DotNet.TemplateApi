using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.App_Start.ModelBinderProviders
{
    public class DateTimeBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateTime) 
                || context.Metadata.ModelType == typeof(DateTime?)
                || context.Metadata.ModelType == typeof(DateTimeInput))
            {
                return new BinderTypeModelBinder(typeof(ModelBinders.DateTimeModelBinder));
            }

            return null;
        }
    }
}
