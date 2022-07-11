using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.ViewsData;
using DotNetCore.API.Template.Site.Extensions;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Site.DataModel.UsuarioDataModel;
using DotNetCore.API.Template.Site.Filters;

namespace DotNetCore.API.Template.Site.Controllers.Servicos
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Usuario[]>))]
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Usuario>))]
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Usuario>))]
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
        [HttpDelete]
        [ReferenciarApp(typeof(UsuarioApp), nameof(UsuarioApp.Excluir))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData))]
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
