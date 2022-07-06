using System.Linq;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace DotNetCore.API.Template.Site.Swashbuckle
{
    public class FiltroOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.ParameterDescriptions
               .Where(d => d.Source.Id == "Query").ToList()
               .ForEach(param =>
               {
                   bool toIgnore =
                       ((DefaultModelMetadata)param.ModelMetadata)
                       .Attributes.PropertyAttributes
                       ?.Any(x => x is JsonIgnoreAttribute) ?? false;

                   if (toIgnore)
                   {
                       string aaa = param.Name;

                       OpenApiParameter[] toRemove = operation.Parameters
                           .Where(p => p.Name == param.Name || p.Name.StartsWith($"{param.Name}.")).ToArray();

                       foreach (OpenApiParameter item in toRemove)
                       {
                           operation.Parameters.Remove(item);
                       }
                   }
               });
        }
    }
}