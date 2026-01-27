using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.Filters;
using TemplateApi.Api.ViewsData.ErroViewData;
using TemplateApi.Api.Extensions;

namespace TemplateApi.Api.Controllers.Html {

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErroController : Common.BaseMvcController {
        public ErroController(
            ILogger<ErroController> logger) {
            _logger = logger;
        }

        private readonly ILogger<ErroController> _logger;

        [IgnorarFiltroAutorizacao]
        [Route("Html/[controller]/{status}")]
        public IActionResult Erro(int status) {
            IndexErroViewData resultado = new(status);
            resultado.ExtrairModelState(ModelState);

            return View(resultado);
        }
    }
}
