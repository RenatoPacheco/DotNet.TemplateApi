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
using DotNetCore.API.Template.Site.Filters;
using System.Collections.Generic;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController, AcessoLivre]
    [Route("[controller]")]
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
        /// Obter um arquivo de storages
        /// </summary>
        [HttpGet, Route("{Alias}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Storage>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "", typeof(byte[]))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(byte[]))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "", typeof(byte[]))]
        public IActionResult Get([FromQuery]ObterStorageDataModel values)
        {
            InvocarSeNulo(ref values);

            ObterStorageCmd cmd = _mapper.Map<ObterStorageCmd>(values);
            cmd.ExtrairModelState(ModelState);

            Storage resultado = _appStorage.Obter(cmd);
            Validate(_appStorage);

            if (IsValid())
                return CustomResponse(resultado);

            return NotFound();
        }
    }
}
