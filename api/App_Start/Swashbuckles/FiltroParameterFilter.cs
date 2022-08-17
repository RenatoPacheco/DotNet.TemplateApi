using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TemplateApi.Api.App_Start.Swashbuckles
{
    public class FiltroParameterFilter : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter?.Schema?.Example != null)
            {
                parameter.Schema.Example = null;
            }
        }
    }
}
