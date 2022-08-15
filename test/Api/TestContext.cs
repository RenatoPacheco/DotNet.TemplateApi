using System.Net.Http;
using TemplateApi.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace TemplateApi.Teste.Api
{
    public class TestContext
    {
        public TestContext()
        {
            _server = new TestServer(
                new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(
                    new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>());
            
            Client = _server.CreateClient();
        }

        private readonly TestServer _server;
        public HttpClient Client { get; private set; }
    }
}
