using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController]
    [Route("servico/[controller]")]
    public class SobreController : ControllerBase
    {
        public SobreController(
            ILogger<SobreController> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<SobreController> _logger;

        [HttpGet]
        public Sobre Get()
        {
            return new Sobre();
        }
    }
}
