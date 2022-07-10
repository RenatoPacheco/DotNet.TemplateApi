using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.ViewsData;
using DotNetCore.API.Template.Site.Extensions;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Site.DataModel.StorageDataModel;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController]
    [Route("servico/[controller]")]
    public class StorageController : Common.BaseController
    {
        public StorageController(
            StorageApp appStorage,
            IMapper mapper,
            ILogger<StorageController> logger)
        {
            _logger = logger;
            _appStorage = appStorage;
            _mapper = mapper;
        }

        private readonly ILogger<StorageController> _logger;
        private readonly StorageApp _appStorage;
        private readonly IMapper _mapper;

        /// <summary>
        /// Filtro de storages
        /// </summary>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Storage[]>))]
        public IActionResult Get([FromQuery] FiltrarStorageDataModel query)
        {
            InvocarSeNulo(ref query);

            FiltrarStorageCmd cmd = _mapper.Map<FiltrarStorageCmd>(query);
            cmd.ExtrairModelState(ModelState);

            ResultadoBusca<Storage> resultado = _appStorage.Filtrar(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Inserir storage
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Storage[]>))]
        public IActionResult Post([FromForm] InserirStorageDataModel body)
        {
            InvocarSeNulo(ref body);

            InserirStorageCmd cmd = _mapper.Map<InserirStorageCmd>(body);
            cmd.ExtrairModelStateParaBody(ModelState);

            Storage[] resultado = _appStorage.Inserir(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar storage
        /// </summary>
        [HttpPatch]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Storage>))]
        public IActionResult Patch([FromBody] EditarStorageDataModel body)
        {
            InvocarSeNulo(ref body);

            EditarStorageCmd cmd = _mapper.Map<EditarStorageCmd>(body);
            cmd.ExtrairModelStateParaBody(ModelState);

            Storage resultado = _appStorage.Editar(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Deletar um ou mais storages
        /// </summary>
        [HttpDelete]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData))]
        public IActionResult Delete([FromQuery] ExcluirStorageDataModel query)
        {
            InvocarSeNulo(ref query);

            ExcluirStorageCmd cmd = _mapper.Map<ExcluirStorageCmd>(query);
            cmd.ExtrairModelState(ModelState);

            _appStorage.Excluir(cmd);
            Validate(_appStorage);

            return CustomResponse();
        }
    }
}
