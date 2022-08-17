using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TemplateApi.Teste.Api
{
    public class TestContext
    {
        public TestContext()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // set up servises
                    });
                });
            Client = application.CreateClient();
        }

        public HttpClient Client { get; private set; }
    }
}
