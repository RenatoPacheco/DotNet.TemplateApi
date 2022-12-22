using System;
using TemplateApi.Recurso;
using System.Threading.Tasks;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class GuidModelBinder : IModelBinder
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

            if (GuidInput.TryParse(value, out GuidInput result))
            {
                bindingContext.Result = ModelBindingResult.Success((Guid)result);
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