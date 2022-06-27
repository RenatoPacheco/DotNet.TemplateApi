using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController]
    [Route("servico/[controller]")]
    public class UsuarioController : ControllerBase
    {
        public UsuarioController(
            ILogger<SobreController> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<SobreController> _logger;

        [HttpGet]
        public IList<Usuario> Get()
        {
            return new List<Usuario>
            {
                new Usuario("Teste", "teste@teste.com", Status.Ativo)
            };
        }

        [HttpPost]
        public Usuario Post()
        {
            return new Usuario("Teste", "teste@teste.com", Status.Ativo);
        }

        [HttpPatch]
        public Usuario Patch()
        {
            return new Usuario("Teste", "teste@teste.com", Status.Ativo);
        }

        [HttpDelete]
        public Usuario Delete()
        {
            return new Usuario("Teste", "teste@teste.com", Status.Ativo);
        }
    }
}
