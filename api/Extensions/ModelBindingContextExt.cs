using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

namespace TemplateApi.Api.Extensions
{
    public static class ModelBindingContextExt
    {
        public static string DisplayName(this ModelBindingContext source)
        {
            string property = Regex.Replace(source.ModelName ?? string.Empty, @"^([^\.]*\.)+", string.Empty);
            string result = source.ModelMetadata.ModelType.DisplayName(property);
            return string.IsNullOrWhiteSpace(result) ? property : result;
        }
    }
}
