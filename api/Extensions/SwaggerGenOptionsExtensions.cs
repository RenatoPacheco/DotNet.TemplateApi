using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TemplateApi.Api.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static IList<IOpenApiAny> OpenApiEnum<T>(this SwaggerGenOptions _)
            where T: struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                            .Select(x => (IOpenApiAny)new OpenApiString($"{x}")).ToList();
        }
    }
}
