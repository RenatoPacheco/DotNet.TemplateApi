using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemplateApi.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Api.DataModels.UsuarioDataModel;
using TemplateApi.Api.DataAnnotations;

namespace TemplateApi.Api.Controllers.Services
{
    [ApiController]
    [Route("Servico/[controller]")]
    public class UsuarioController : Common.BaseController
    {
        public UsuarioController(
            UsuarioApp appUsuario,
            IMapper mapper,
            ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _appUsuario = appUsuario;
            _mapper = mapper;
        }

        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioApp _appUsuario;
        private readonly IMapper _mapper;

        /// <summary>
        /// Filtro de usuários
        /// </summary>
        [HttpGet]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Filtrar))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<ResultadoBusca<Usuario>>))]
        public IActionResult Get([FromQuery] FiltrarUsuarioDataModel query)
        {
            InvocarSeNulo(ref query);

            FiltrarUsuarioCmd cmd = _mapper.Map<FiltrarUsuarioCmd>(query);
            cmd.ExtrairModelState(ModelState);

            ResultadoBusca<Usuario> resultado = _appUsuario.Filtrar(cmd);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Inserir usuário
        /// </summary>
        [HttpPost]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Inserir))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<Usuario>))]
        public IActionResult Post([FromBody] InserirUsuarioDataModel body)
        {
            InvocarSeNulo(ref body);

            InserirUsuarioCmd cmd = _mapper.Map<InserirUsuarioCmd>(body);
            cmd.ExtrairModelStateParaBody(ModelState);

            Usuario resultado = _appUsuario.Inserir(cmd);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar usuário
        /// </summary>
        [HttpPatch]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Editar))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<Usuario>))]
        public IActionResult Patch([FromBody] EditarUsuarioDataModel body)
        {
            InvocarSeNulo(ref body);

            EditarUsuarioCmd cmd = _mapper.Map<EditarUsuarioCmd>(body);
            cmd.ExtrairModelStateParaBody(ModelState);

            Usuario resultado = _appUsuario.Editar(cmd);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData))]
        public IActionResult Delete([FromQuery] ExcluirUsuarioDataModel query)
        {
            InvocarSeNulo(ref query);

            ExcluirUsuarioCmd cmd = _mapper.Map<ExcluirUsuarioCmd>(query);
            cmd.ExtrairModelState(ModelState);

            _appUsuario.Excluir(cmd);
            Validate(_appUsuario);

            return CustomResponse();
        }
    }
}
