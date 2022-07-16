using System;
using DotNetCore.API.Template.Recurso;
using System.Threading.Tasks;
using System.Globalization;
using DotNetCore.API.Template.Site.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetCore.API.Template.Site.Settings.ModelBinder
{
    public class DateTimeModelBinder : IModelBinder
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

            if (TryParse(value, out DateTime result))
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

        public bool TryParse(string value, out DateTime result)
        {
            DateTimeStyles styles = DateTimeStyles.NoCurrentDateDefault;
            IFormatProvider provider = null;
            string[] formats = new string[]
            {
                "dd/MM/yyyy HH:mm:ss",
                "dd/MM/yyyy",
                "dd-MM-yyyy HH:mm:ss",
                "dd-MM-yyyy",
                "yyyy/MM/dd HH:mm:ss",
                "yyyy/MM/dd",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd"
            };

            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(
                            value,
                            format,
                            provider,
                            styles,
                            out result))
                {
                    return true;
                }
            }

            result = new DateTime();
            return false;
        }
    }
}