using System.Net;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Aplicacoes;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Api.DataModels.UsuarioDataModel;
using TemplateApi.Api.DataAnnotations;

namespace TemplateApi.Api.Controllers.Services {

    [ApiController]
    [Route("Servico/[controller]")]
    [ApiExplorerSettings(GroupName = "Usuário")]
    public class UsuarioController : Common.BaseApiController {

        public UsuarioController(
            UsuarioApp appUsuario,
            ILogger<UsuarioController> logger) {
            _logger = logger;
            _appUsuario = appUsuario;
        }

        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioApp _appUsuario;

        /// <summary>
        /// Filtro de usuários
        /// </summary>
        [HttpGet]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Filtrar))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(BuscaViewData<Usuario>))]
        public IActionResult Get([FromQuery] FiltrarUsuarioDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelState(ModelState);

            var cmd = query.Montar();
            var resultado = _appUsuario.Filtrar(cmd);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Inserir usuário
        /// </summary>
        [HttpPost]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Inserir))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<Usuario>))]
        public IActionResult Post([FromBody] InserirUsuarioDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelState(ModelState);

            var cmd = body.Montar();
            var resultado = _appUsuario.Inserir(cmd);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar usuário
        /// </summary>
        [HttpPatch]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Editar))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<Usuario>))]
        public IActionResult Patch([FromBody] EditarUsuarioDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelState(ModelState);

            var cmd = body.Montar();
            var resultado = _appUsuario.Editar(cmd);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Deletar um ou mais usuários
        /// </summary>
        /// <remarks>
        /// <p>Permite excluir um ou mais usuários, que na verdade não são excluídos do banco, só são alterados para o status de excluído.</p>
        /// </remarks>
        [HttpDelete]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Excluir))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData))]
        public IActionResult Delete([FromQuery] ExcluirUsuarioDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelState(ModelState);

            var cmd = query.Montar();
            _appUsuario.Excluir(cmd);
            Validate(_appUsuario);

            return CustomResponse();
        }
    }
}
