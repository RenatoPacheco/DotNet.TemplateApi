using TemplateApi.Recurso;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;
using System.Threading.Tasks;
using System;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class TimeSpanModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if ((bindingContext.ModelType != typeof(TimeSpan?)
                && bindingContext.ModelType != typeof(TimeSpan)
                && bindingContext.ModelType != typeof(TimeSpanInput))
                || bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                return Task.CompletedTask;
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue?.Trim();

            if (string.IsNullOrEmpty(value))
            {
                if (bindingContext.ModelType == typeof(TimeSpan) || value == string.Empty)
                {
                    bindingContext.SetStateError(AvisosResx.XNaoEhValido);
                }

                return Task.CompletedTask;
            }

            if (TimeSpanInput.TryParse(value, out TimeSpanInput result))
            {
                if (bindingContext.ModelType == typeof(TimeSpanInput))
                    bindingContext.Result = ModelBindingResult.Success(result);
                else
                    bindingContext.Result = ModelBindingResult.Success((TimeSpan)result);
            }
            else if (!bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                bindingContext.SetStateError(AvisosResx.XNaoEhValido);
            }

            return Task.CompletedTask;
        }
    }
}