using BitHelp.Core.Type.pt_BR;
using TemplateApi.Recurso;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Api.Extensions;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class PhoneTypeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if ((bindingContext.ModelType != typeof(PhoneType?)
                && bindingContext.ModelType != typeof(PhoneType))
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
                if (bindingContext.ModelType == typeof(PhoneType) || value == string.Empty)
                {
                    bindingContext.SetStateError(AvisosResx.XNaoEhValido);
                }

                return Task.CompletedTask;
            }

            if (PhoneType.TryParse(value, out PhoneType result))
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else if (!bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                bindingContext.SetStateError(AvisosResx.XNaoEhValido);
            }

            return Task.CompletedTask;
        }
    }
}