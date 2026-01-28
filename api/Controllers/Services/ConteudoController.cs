using System.Net;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Aplicacoes;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Api.DataModels.ConteudoDataModel;
using TemplateApi.Api.DataAnnotations;

namespace TemplateApi.Api.Controllers.Services {

    [ApiController]
    [Route("Servico/[controller]")]
    [ApiExplorerSettings(GroupName = "Conteúdo")]
    public class ConteudoController : Common.BaseApiController {
        public ConteudoController(
            ConteudoApp appConteudo,
            ILogger<ConteudoController> logger) {
            _logger = logger;
            _appConteudo = appConteudo;
        }

        private readonly ILogger<ConteudoController> _logger;
        private readonly ConteudoApp _appConteudo;

        /// <summary>
        /// Filtro de conteúdos
        /// </summary>
        [HttpGet]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Filtrar))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(BuscaViewData<Conteudo>))]
        public IActionResult Get([FromQuery] FiltrarConteudoDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelState(ModelState);

            var cmd = query.Montar();
            var resultado = _appConteudo.Filtrar(cmd);
            Validate(_appConteudo);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Inserir conteúdo
        /// </summary>
        [HttpPost]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Inserir))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<Conteudo>))]
        public IActionResult Post([FromBody] InserirConteudoDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelState(ModelState);

            var cmd = body.Montar();
            var resultado = _appConteudo.Inserir(cmd);
            Validate(_appConteudo);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar conteúdo
        /// </summary>
        [HttpPatch]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Editar))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<Conteudo>))]
        public IActionResult Patch([FromBody] EditarConteudoDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelState(ModelState);

            var cmd = body.Montar();
            var resultado = _appConteudo.Editar(cmd);
            Validate(_appConteudo);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Deletar um ou mais conteúdos
        /// </summary>
        /// <remarks>
        /// <p>Permite excluir um ou mais conteúdos, que na verdade não são excluídos do banco, só são alterados para o status de excluído.</p>
        /// </remarks>
        [HttpDelete]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Excluir))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData))]
        public IActionResult Delete([FromQuery] ExcluirConteudoDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelState(ModelState);

            var cmd = query.Montar();
            _appConteudo.Excluir(cmd);
            Validate(_appConteudo);

            return CustomResponse();
        }
    }
}
