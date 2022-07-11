using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.ViewsData;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Site.Filters;
using DotNetCore.API.Template.Dominio.Entidades;

namespace DotNetCore.API.Template.Site.Controllers.Servicos
{
    [ApiController, AcessoLivre]
    [Route("Servico/[controller]")]
    public class AutenticacaoController : Common.BaseController
    {
        public AutenticacaoController(
            AutenticacaoApp appAutenticacao,
            ILogger<AutenticacaoController> logger)
        {
            _logger = logger;
            _appAutenticacao = appAutenticacao;
        }

        private readonly ILogger<AutenticacaoController> _logger;
        private readonly AutenticacaoApp _appAutenticacao;

        /// <summary>
        /// Obter os dados da autenticação atual
        /// </summary>
        [HttpGet]
        [ReferenciarApp(typeof(AutenticacaoApp), nameof(AutenticacaoApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Autenticacao>))]
        public IActionResult Get()
        {
            Autenticacao resultado = _appAutenticacao.Obter();
            Validate(_appAutenticacao);

            return CustomResponse(resultado);
        }
    }
}
