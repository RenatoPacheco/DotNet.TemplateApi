using TemplateApi.Api.Helpers;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Api.Extensions;

namespace TemplateApi.Api
{
    public static class MiddlewareConfig
    {
        public static void Config(IApplicationBuilder app)
        {
            app.UseStatusCodePages(context => {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;
                var path = request.Path.ToString().ToLower();

                if (response.StatusCode >= 300)
                {
                    if (path.StartsWith("/servico/"))
                    {
                        response.WriteAsJsonAsync(new ComumViewData {
                            Avisos = new Avisos(response.StatusCode)
                        }, ConfiguracaoJsonText.Leitura());
                    }
                    else
                    {
                        context.HttpContext.RedirectToErrorPage();
                    }
                }

                return Task.CompletedTask;
            });
        }
    }
}
