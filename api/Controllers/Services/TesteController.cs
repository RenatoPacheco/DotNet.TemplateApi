using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Api.DataModels.TesteDataModel;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.ViewsData;
using TemplateApi.Aplicacoes;
using TemplateApi.Dominio.Comandos.TesteCmds;
using TemplateApi.Dominio.Notacoes;

namespace TemplateApi.Api.Controllers.Services {

    [ApiController, NaoRequerAutorizacao]
    [Route("Servico/[controller]")]
    public class TesteController : Common.BaseApiController {
        public TesteController(
            TesteApp appTeste,
            ILogger<TesteController> logger) {
            _logger = logger;
            _appTeste = appTeste;
        }

        private readonly ILogger<TesteController> _logger;
        private readonly TesteApp _appTeste;

        /// <summary>
        /// Recebendo os dados por FromQuery
        /// </summary>
        [Route("FromQuery")]
        [ApiExplorerSettings(GroupName = "Teste / FromQuery")]
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete]
        [ReferenciarApp(typeof(TesteApp), nameof(TesteApp.Formatos))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromQuery([FromQuery] FormatosTesteDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelState(ModelState);

            var cmd = query.Montar();
            var resultado = _appTeste.Formatos(cmd);
            Validate(_appTeste);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Recebendo os dados por FromBody
        /// </summary>
        [Route("FromBody")]
        [ApiExplorerSettings(GroupName = "Teste / FromBody")]
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete]
        [ReferenciarApp(typeof(TesteApp), nameof(TesteApp.Formatos))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromBody([FromBody] FormatosTesteDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelState(ModelState);

            var cmd = body.Montar();
            var resultado = _appTeste.Formatos(cmd);
            Validate(_appTeste);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Recebendo os dados por FromForm
        /// </summary>
        [Route("FromForm")]
        [ApiExplorerSettings(GroupName = "Teste / FromForm")]
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete]
        [ReferenciarApp(typeof(TesteApp), nameof(TesteApp.Formatos))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromForm([FromForm] FormatosTesteDataModel form) {

            InvocarSeNulo(ref form);
            form.ExtrairModelState(ModelState);

            var cmd = form.Montar();
            var resultado = _appTeste.Formatos(cmd);
            Validate(_appTeste);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Recebendo os dados por FromHeader
        /// </summary>
        [Route("FromHeader")]
        [ApiExplorerSettings(GroupName = "Teste / FromHeader")]
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete]
        [ReferenciarApp(typeof(TesteApp), nameof(TesteApp.Formatos))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromHeader([FromHeader] FormatosTesteDataModel header) {

            InvocarSeNulo(ref header);
            header.ExtrairModelState(ModelState);

            var cmd = header.Montar();
            var resultado = _appTeste.Formatos(cmd);
            Validate(_appTeste);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Recebendo os dados ser usar nenhum dos tipos From
        /// </summary>
        [Route("WithoutFrom")]
        [ApiExplorerSettings(GroupName = "Teste / WithoutFrom")]
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete]
        [ReferenciarApp(typeof(TesteApp), nameof(TesteApp.Formatos))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult WithoutFrom(FormatosTesteDataModel without) {

            InvocarSeNulo(ref without);
            without.ExtrairModelState(ModelState);

            var cmd = without.Montar();
            var resultado = _appTeste.Formatos(cmd);
            Validate(_appTeste);

            return CustomResponse(resultado);
        }
    }
}
