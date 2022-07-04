using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Site.Filters;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController, AcessoLivre]
    [Route("servico/[controller]")]
    public class UsuarioController : Common.BaseController
    {
        public UsuarioController(
            UsuarioApp appUsuario,
            ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _appUsuario = appUsuario;
        }

        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioApp _appUsuario;

        [HttpGet]
        public IActionResult Get([FromQuery] FiltrarUsuarioCmd query)
        {
            Notifications.Clear();

            Usuario[] resultado = _appUsuario.Filtrar(query);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        [HttpPost]
        public IActionResult Post([FromBody] InserirUsuarioCmd body)
        {
            Notifications.Clear();

            Usuario resultado = _appUsuario.Inserir(body);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] EditarUsuarioCmd body)
        {
            Notifications.Clear();

            Usuario resultado = _appUsuario.Editar(body);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] ExcluirUsuarioCmd query)
        {
            Notifications.Clear();

            _appUsuario.Excluir(query);
            Validate(_appUsuario);

            return CustomResponse();
        }
    }
}
