using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Dominio.ObjetosDeValor;

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

        public static void MapTypeEnum<T>(this SwaggerGenOptions source)
            where T : struct
        {
            source.MapType<T>(
                () => new OpenApiSchema
                {
                    Type = "string",
                    Enum = source.OpenApiEnum<T>()
                });

            source.MapType<T?>(
                () => new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Enum = source.OpenApiEnum<T>()
                });

            source.MapType<EnumInput<T>>(
                () => new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Enum = source.OpenApiEnum<T>()
                });
        }
    }
}
