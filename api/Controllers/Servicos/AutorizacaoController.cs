using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.Filters;
using DotNetCore.API.Template.Site.ViewsData;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.Controllers.Servicos
{
    [ApiController, AcessoLivre]
    [Route("Servico/[controller]")]
    public class AutorizacaoController : Common.BaseController
    {
        public AutorizacaoController(
            AutorizacaoApp appAutorizacao,
            ILogger<AutorizacaoController> logger)
        {
            _logger = logger;
            _appAutorizacao = appAutorizacao;
        }

        private readonly ILogger<AutorizacaoController> _logger;
        private readonly AutorizacaoApp _appAutorizacao;

        /// <summary>
        /// Listar todas autorizações existentes
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Autorizacao>))]
        public IActionResult Get()
        {
            Autorizacao[] resultado = _appAutorizacao.Listar();
            Validate(_appAutorizacao);

            return CustomResponse(resultado);
        }
    }
}
