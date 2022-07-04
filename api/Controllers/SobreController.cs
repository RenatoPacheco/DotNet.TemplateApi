using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Site.Filters;

namespace DotNetCore.API.Template.Site.Controllers
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

        [HttpGet]
        public IActionResult Get()
        {
            Notifications.Clear();

            Sobre resultado = _appSobre.Obter();
            Validate(_appSobre);

            return CustomResponse(resultado);
        }
    }
}
