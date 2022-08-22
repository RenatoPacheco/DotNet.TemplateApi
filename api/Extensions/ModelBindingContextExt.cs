using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TemplateApi.Api.Extensions
{
    public static class ModelBindingContextExt
    {
        public static string DisplayName(this ModelBindingContext bindingContext)
        {
            string result = bindingContext.ModelMetadata.ModelType.DisplayName(bindingContext.ModelName);
            return string.IsNullOrWhiteSpace(result) ? bindingContext.ModelName : result;
        }
    }
}
