using System.Net;
using AutoMapper;
using TemplateApi.Aplicacao;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.Extensions;
using Microsoft.Extensions.Logging;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Api.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Dominio.Comandos.TesteCmds;
using TemplateApi.Api.DataModels.TesteDataModel;

namespace TemplateApi.Api.Controllers.Services
{
    [ApiController, NaoRequerAutorizacao]
    [Route("Servico/[controller]")]
    public class TesteController : Common.BaseController
    {
        public TesteController(
            IMapper mapper,
            TesteApp appTeste,
            ILogger<TesteController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _appTeste = appTeste;
        }

        private readonly ILogger<TesteController> _logger;
        private readonly TesteApp _appTeste;
        private readonly IMapper _mapper;

        /// <summary>
        /// Recebendo os dados por FromQuery
        /// </summary>
        [Route("FromQuery")]
        [ApiExplorerSettings(GroupName = "Teste / FromQuery")]
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpDelete]
        [ReferenciarApp(typeof(TesteApp), nameof(TesteApp.Formatos))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromQuery([FromQuery] FormatosTesteDataModel query, [FromQuery]decimal? teste)
        {
            InvocarSeNulo(ref query);

            FormatosTesteCmd cmd = _mapper.Map<FormatosTesteCmd>(query);
            cmd.ExtrairModelState(ModelState);

            FormatosTesteCmd resultado = _appTeste.Formatos(cmd);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromBody([FromBody] FormatosTesteDataModel body)
        {
            InvocarSeNulo(ref body);

            FormatosTesteCmd cmd = _mapper.Map<FormatosTesteCmd>(body);
            cmd.ExtrairModelState(ModelState);

            FormatosTesteCmd resultado = _appTeste.Formatos(cmd);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromForm([FromForm] FormatosTesteDataModel form)
        {
            InvocarSeNulo(ref form);

            FormatosTesteCmd cmd = _mapper.Map<FormatosTesteCmd>(form);
            cmd.ExtrairModelState(ModelState);

            FormatosTesteCmd resultado = _appTeste.Formatos(cmd);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult FromHeader([FromHeader] FormatosTesteDataModel header)
        {
            InvocarSeNulo(ref header);

            FormatosTesteCmd cmd = _mapper.Map<FormatosTesteCmd>(header);
            cmd.ExtrairModelState(ModelState);

            FormatosTesteCmd resultado = _appTeste.Formatos(cmd);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<FormatosTesteCmd>))]
        public IActionResult WithoutFrom(FormatosTesteDataModel without)
        {
            InvocarSeNulo(ref without);

            FormatosTesteCmd cmd = _mapper.Map<FormatosTesteCmd>(without);
            cmd.ExtrairModelState(ModelState);

            FormatosTesteCmd resultado = _appTeste.Formatos(cmd);
            Validate(_appTeste);

            return CustomResponse(resultado);
        }
    }
}
