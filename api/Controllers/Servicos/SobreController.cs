using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using DotNetCore.API.Template.Site.Filters;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.ViewsData;

namespace DotNetCore.API.Template.Site.Controllers.Servicos
{
    [ApiController, AcessoLivre]
    [Route("servico/[controller]")]
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
        /// Essa consulta vai permitir obter dados como:
        /// 
        /// <code>
        /// "dados": {
        ///     "nome": "Projeto teste API",
        ///     "versao": "1.0.0",
        ///     "servidor": "local"
        /// } 
        /// </code>
        /// 
        /// </remarks>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Sobre>))]
        public IActionResult Get()
        {
            Sobre resultado = _appSobre.Obter();
            Validate(_appSobre);

            return CustomResponse(resultado);
        }
    }
}
