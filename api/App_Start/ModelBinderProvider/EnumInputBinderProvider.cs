using System;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Api.App_Start.ModelBinder;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace TemplateApi.Api.App_Start.ModelBinderProvider
{
    public class EnumInputBinderProvider<T> : IModelBinderProvider
        where T : struct
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(EnumInput<T>))
            {
                return new BinderTypeModelBinder(typeof(EnumInputModelBinder<T>));
            }

            return null;
        }
    }
}
