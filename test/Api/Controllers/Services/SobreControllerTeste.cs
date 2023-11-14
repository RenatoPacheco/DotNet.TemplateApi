using FluentAssertions;
using TemplateApi.Api.ViewsData;
using TemplateApi.Teste.Extensions;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Teste.Api.Controllers.Services
{
    public class SobreControllerTeste
    {
        public SobreControllerTeste()
        {
            _testContext = new TestContext();
        }

        private readonly TestContext _testContext;
        private HttpClient Client => _testContext.Client;

        [Fact]
        public async Task Obter_sobre_atual_status_code_ok()
        {
            HttpResponseMessage response = await Client.GetAsync("servico/sobre");
            ComumViewData<Sobre> result = await response.Content.ReadAsViewDataAsync<Sobre>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
