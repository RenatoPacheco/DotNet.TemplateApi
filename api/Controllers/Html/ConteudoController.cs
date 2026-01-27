using TemplateApi.Aplicacoes;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Api.Controllers.Html {
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Html/[controller]/[action]/{id?}")]
    public class ConteudoController : Common.BaseMvcController {
        public ConteudoController(
            ConteudoApp appConteudo,
            ILogger<ConteudoController> logger) {
            _logger = logger;
            _appConteudo = appConteudo;
        }

        private readonly ILogger<ConteudoController> _logger;
        private readonly ConteudoApp _appConteudo;

        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Filtrar))]
        public IActionResult Index(int id) {
            Conteudo resultado = _appConteudo.Filtrar(new FiltrarConteudoCmd {
                Maximo = 1,
                Conteudo = new List<int> { id },
                Contexto = ContextoCmd.Visualizar
            }).FirstOrDefault();
            Validate(_appConteudo);

            return View();
        }
    }
}
