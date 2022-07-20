using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemplateApi.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.Controllers.Servicos
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
        ///     <li><strong>servidor:</strong> tipo de servidor que a aplicação está rodando, que pode ser: 
        ///     <strong>local</strong>, <strong>homologacao</strong> ou <strong>producao</strong></li>
        /// </ul>
        /// </remarks>
        [HttpGet]
        [ReferenciarApp(typeof(SobreApp), nameof(SobreApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Sobre>))]
        public IActionResult Get()
        {
            Sobre resultado = _appSobre.Obter();
            Validate(_appSobre);

            return CustomResponse(resultado);
        }
    }
}
