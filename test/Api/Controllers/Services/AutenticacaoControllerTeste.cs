using FluentAssertions;

namespace TemplateApi.Teste.Api.Controllers.Services
{
    public class AutenticacaoControllerTeste
    {
        public AutenticacaoControllerTeste()
        {
            _testContext = new TestContext();
        }

        private readonly TestContext _testContext;
        private HttpClient Client => _testContext.Client;

        [Fact]
        public async Task Obter_autenticacao_atual_status_code_ok()
        {
            HttpResponseMessage response = await Client.GetAsync("servico/autenticacao");
            string json = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Alterar_autenticacao_core_atual_status_code_0k()
        {
            HttpResponseMessage response = await Client.GetAsync("servico/autenticacao/core");
            string json = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
