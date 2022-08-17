using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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
                    builder.ConfigureAppConfiguration((context, builder) => 
                    {
                        
                    });
                    builder.ConfigureServices(services =>
                    {
                        // set up servises
                    });
                });
            var client = application.CreateClient();
        }

        public HttpClient Client { get; private set; }
    }
}
