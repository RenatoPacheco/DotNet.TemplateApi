using TemplateApi.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.Controllers.Html
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("Html/[controller]/[action]/{id?}")]
    public class SobreController : Common.BaseMvcController
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

        [ReferenciarApp(typeof(SobreApp), nameof(SobreApp.Obter))]
        public IActionResult Index()
        {
            Sobre resultado = _appSobre.Obter();
            Validate(_appSobre);

            return View();
        }
    }
}
