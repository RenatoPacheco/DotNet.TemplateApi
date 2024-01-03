using TemplateApi.Recurso;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;
using System.Threading.Tasks;
using System;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if ((bindingContext.ModelType != typeof(DateTime?)
                && bindingContext.ModelType != typeof(DateTime)
                && bindingContext.ModelType != typeof(DateTimeInput))
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
                if (bindingContext.ModelType == typeof(DateTime) || value == string.Empty)
                {
                    bindingContext.SetStateError(AvisosResx.XNaoEhValido);
                }

                return Task.CompletedTask;
            }

            if (DateTimeInput.TryParse(value, out DateTimeInput result))
            {
                if (bindingContext.ModelType == typeof(DateTimeInput))
                    bindingContext.Result = ModelBindingResult.Success(result);
                else
                    bindingContext.Result = ModelBindingResult.Success((DateTime)result);
            }
            else if (!bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                bindingContext.SetStateError(AvisosResx.XNaoEhValido);
            }

            return Task.CompletedTask;
        }
    }
}