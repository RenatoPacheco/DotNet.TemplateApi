using System;
using System.Threading.Tasks;
using TemplateApi.RecursoResx;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TemplateApi.Api.Settings.ModelBinder
{
    public class EnumModelBinder<T> : IModelBinder
        where T : struct
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

            if (Enum.TryParse(value, true, out T result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else if(!bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
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