using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TemplateApi.Api.Extensions
{
    public static class SwaggerGenOptionsExt
    {
        public static IList<IOpenApiAny> OpenApiEnum<T>(this SwaggerGenOptions _)
            where T: struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                            .Select(x => (IOpenApiAny)new OpenApiString($"{x}")).ToList();
        }

        public static OpenApiString OpenApiEnumExample<T>(this SwaggerGenOptions _)
            where T : struct
        {
            return new OpenApiString(Enum.GetValues(typeof(T)).Cast<T>().First().ToString());
        }
    }
}
