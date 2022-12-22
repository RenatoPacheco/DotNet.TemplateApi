using System;
using TemplateApi.Recurso;
using System.Threading.Tasks;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class EnumInputModelBinder<T> : IModelBinder
        where T: struct
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (EnumInput<T>.TryParse(value, out EnumInput<T> result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else if (!bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
                bindingContext.ModelState.AddModelError(
                bindingContext.ModelName,
                string.Format(AvisosResx.XNaoEhValido,
                bindingContext.DisplayName()));
            }

            return Task.CompletedTask;
        }
    }
}