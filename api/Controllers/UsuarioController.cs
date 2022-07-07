using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using DotNetCore.API.Template.Site.Extensions;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;
using DotNetCore.API.Template.Site.DataModel.UsuarioDataModel;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController]
    [Route("servico/[controller]")]
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
        public IActionResult Post([FromBody] InserirUsuarioCmd body)
        {
            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            Usuario resultado = _appUsuario.Inserir(body);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar usuário
        /// </summary>
        [HttpPatch]
        public IActionResult Patch([FromBody] EditarUsuarioCmd body)
        {
            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            Usuario resultado = _appUsuario.Editar(body);
            Validate(_appUsuario);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Deletar um ou mais usuários
        /// </summary>
        [HttpDelete]
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
