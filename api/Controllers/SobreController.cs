using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController]
    [Route("servico/[controller]")]
    public class SobreController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<SobreController> _logger;

        public SobreController(
            ILogger<SobreController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Sobre Get()
        {
            return new Sobre();
        }
    }
}
