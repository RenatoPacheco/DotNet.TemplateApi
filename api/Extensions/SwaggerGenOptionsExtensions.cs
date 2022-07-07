using System;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;

namespace DotNetCore.API.Template.Site.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static IList<IOpenApiAny> IOpenApiAnyByEnum<T>(this SwaggerGenOptions _)
            where T: struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(x => OpenApiAnyFactory.CreateFor(
                       new OpenApiSchema() { Type = "string" }, x.ToString()
                   )).ToList();
        }
    }
}
