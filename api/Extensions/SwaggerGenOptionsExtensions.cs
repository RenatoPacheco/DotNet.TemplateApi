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
        public static OpenApiSchema SchemaEnum<T>(this SwaggerGenOptions _, string type)
            where T: struct
        {
            return new OpenApiSchema {
                Type = type,
                Enum = Enum.GetValues(typeof(T)).Cast<T>().Select(x => OpenApiAnyFactory.CreateFor(
                       new OpenApiSchema() { Type = type }, x.ToString()
                   )).ToList()
            };
        }

        public static OpenApiSchema SchemaBasic(this SwaggerGenOptions _, string type, object example, object @default = null)
        {
            return new OpenApiSchema {
                Type = type,
                Default = OpenApiAnyFactory.CreateFor(new OpenApiSchema() { Type = type }, @default),
                Example = OpenApiAnyFactory.CreateFor(new OpenApiSchema() { Type = type }, example)
            };
        }
    }
}
