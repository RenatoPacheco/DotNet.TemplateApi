using System;
using TemplateApi.Recurso;
using System.Threading.Tasks;
using TemplateApi.Api.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.App_Start.ModelBinders
{
    public class DoubleModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if ((bindingContext.ModelType != typeof(double?)
                && bindingContext.ModelType != typeof(double)
                && bindingContext.ModelType != typeof(DoubleInput))
                || bindingContext.ModelState.ContainsKey(bindingContext.ModelName))
            {
                return Task.CompletedTask;
            }

            string modelName = bindingContext.ModelName;
            Type modelType = bindingContext.ModelType;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            string value = valueProviderResult.FirstValue;

            if (value == null)
            {
                if (modelType == typeof(double?) || modelType == typeof(DoubleInput))
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                }
                else
                {
                    ErrorReport(bindingContext, valueProviderResult);
                }
            }
            else if (DoubleInput.TryParse(value, out DoubleInput result))
            {
                if (modelType == typeof(DoubleInput))
                    bindingContext.Result = ModelBindingResult.Success(result);
                else
                    bindingContext.Result = ModelBindingResult.Success((double)result);
            }
            else
            {
                ErrorReport(bindingContext, valueProviderResult);
            }

            return Task.CompletedTask;
        }

        protected void ErrorReport(
            ModelBindingContext bindingContext,
            ValueProviderResult valueProviderResult)
        {
            bindingContext.ModelState.SetModelValue(
                bindingContext.ModelName, valueProviderResult);

            bindingContext.ModelState.AddModelError(
                bindingContext.ModelName,
                string.Format(AvisosResx.XNaoEhValido,
                bindingContext.DisplayName()));
        }
    }
}