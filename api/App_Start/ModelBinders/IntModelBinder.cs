using System;
using TemplateApi.Recurso;
using System.Threading.Tasks;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class IntModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if ((bindingContext.ModelType != typeof(int?)
                && bindingContext.ModelType != typeof(int)
                && bindingContext.ModelType != typeof(IntInput))
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
                if (bindingContext.ModelType == typeof(int) || value == string.Empty)
                {
                    bindingContext.SetStateError(AvisosResx.XNaoEhValido);
                }

                return Task.CompletedTask;
            }

            if (IntInput.TryParse(value, out IntInput result))
            {
                if (bindingContext.ModelType == typeof(IntInput))
                    bindingContext.Result = ModelBindingResult.Success(result);
                else
                    bindingContext.Result = ModelBindingResult.Success((int)result);
            }
            else if(!bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                bindingContext.SetStateError(AvisosResx.XNaoEhValido);
            }

            return Task.CompletedTask;
        }
    }
}