using System.Linq;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace DotNetCore.API.Template.Site.Swashbuckle
{
    public class FiltroRequestBodyFilter : IRequestBodyFilter
    {
        public void Apply(OpenApiRequestBody requestBody, RequestBodyFilterContext context)
        {
            context?.FormParameterDescriptions
                ?.Where(d => d.Source.Id == "Form").ToList()
                .ForEach(param =>
                {
                    bool toIgnore =
                       ((DefaultModelMetadata)param.ModelMetadata)
                       .Attributes.PropertyAttributes
                       ?.Any(x => x is JsonIgnoreAttribute) ?? false;

                    if (toIgnore)
                    {
                        OpenApiMediaType openApiMediaType = requestBody.Content.SingleOrDefault(x => x.Key == "multipart/form-data").Value;

                        if (!(openApiMediaType is null))
                        {
                            foreach (string item in openApiMediaType.Encoding.Keys.Where(
                                x => x == param.Name || x.StartsWith($"{param.Name}.")))
                            {
                                openApiMediaType.Schema.Properties.Remove(item);
                            }

                            foreach (string item in openApiMediaType.Schema.Properties.Keys.Where(
                                x => x == param.Name || x.StartsWith($"{param.Name}.")))
                            {
                                openApiMediaType.Encoding.Remove(item);
                            }
                        }

                    }
                });
        }
    }
}
