using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Aplicacao;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.Controllers.Services
{
    [ApiController, NaoRequerAutorizacao]
    [Route("Servico/[controller]")]
    public class SobreController : Common.BaseController
    {
        public SobreController(
            SobreApp appSobre,
            ILogger<SobreController> logger)
        {
            _logger = logger;
            _appSobre = appSobre;
        }

        private readonly ILogger<SobreController> _logger;
        private readonly SobreApp _appSobre;

        /// <summary>
        /// Obter informação como nome e versão da API
        /// </summary>
        /// <remarks>
        /// <p>Essa consulta vai permitir obter dados como:</p>
        /// <ul>
        ///     <li><strong>nome:</strong> nome da aplicação</li>
        ///     <li><strong>versão:</strong> número da versão atual da aplicação</li>
        ///     <li><strong>ambiente:</strong> informa o tipo de ambiente que pode ser producao ou desenvolvimento</li>
        ///     <li><strong>ehDesenvolvimento:</strong> informa se é ambiende de desenvolvimento</li>
        /// </ul>
        /// </remarks>
        [HttpGet]
        [ReferenciarApp(typeof(SobreApp), nameof(SobreApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<Sobre>))]
        public IActionResult Get()
        {
            Sobre resultado = _appSobre.Obter();
            Validate(_appSobre);

            return CustomResponse(resultado);
        }
    }
}
