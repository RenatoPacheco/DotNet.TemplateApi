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
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Api.DataModel.ConteudoDataModel;
using TemplateApi.Api.DataAnnotations;

namespace TemplateApi.Api.Controllers.Services
{
    [ApiController]
    [Route("Servico/[controller]")]
    public class ConteudoController : Common.BaseController
    {
        public ConteudoController(
            IMapper mapper,
            ConteudoApp appConteudo,
            ILogger<ConteudoController> logger)
        {
            _logger = logger;
            _appConteudo = appConteudo;
            _mapper = mapper;
        }

        private readonly ILogger<ConteudoController> _logger;
        private readonly ConteudoApp _appConteudo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Filtro de conteúdos
        /// </summary>
        [HttpGet]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Filtrar))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<ResultadoBusca<Conteudo>>))]
        public IActionResult Get([FromQuery] FiltrarConteudoDataModel query)
        {
            InvocarSeNulo(ref query);

            FiltrarConteudoCmd cmd = _mapper.Map<FiltrarConteudoCmd>(query);
            cmd.ExtrairModelState(ModelState);

            ResultadoBusca<Conteudo> resultado = _appConteudo.Filtrar(cmd);
            Validate(_appConteudo);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Inserir conteúdo
        /// </summary>
        [HttpPost]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Inserir))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Conteudo>))]
        public IActionResult Post([FromBody] InserirConteudoDataModel body)
        {
            InvocarSeNulo(ref body);

            InserirConteudoCmd cmd = _mapper.Map<InserirConteudoCmd>(body);
            cmd.ExtrairModelStateParaBody(ModelState);

            Conteudo resultado = _appConteudo.Inserir(cmd);
            Validate(_appConteudo);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar conteúdo
        /// </summary>
        [HttpPatch]
        [ReferenciarApp(typeof(ConteudoApp), nameof(ConteudoApp.Editar))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Conteudo>))]
        public IActionResult Patch([FromBody] EditarConteudoDataModel body)
        {
            InvocarSeNulo(ref body);

            EditarConteudoCmd cmd = _mapper.Map<EditarConteudoCmd>(body);
            cmd.ExtrairModelStateParaBody(ModelState);

            Conteudo resultado = _appConteudo.Editar(cmd);
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
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData))]
        public IActionResult Delete([FromQuery] ExcluirConteudoDataModel query)
        {
            InvocarSeNulo(ref query);

            ExcluirConteudoCmd cmd = _mapper.Map<ExcluirConteudoCmd>(query);
            cmd.ExtrairModelState(ModelState);

            _appConteudo.Excluir(cmd);
            Validate(_appConteudo);

            return CustomResponse();
        }
    }
}
