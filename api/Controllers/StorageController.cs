using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using DotNetCore.API.Template.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.Filters;
using DotNetCore.API.Template.Site.ViewsData;
using DotNetCore.API.Template.Site.Extensions;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Site.DataModel.StorageDataModel;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController, AcessoLivre]
    [Route("[controller]")]
    public class StorageController : Common.BaseController
    {
        public StorageController(
            StorageApp appStorage,
            IMapper mapper,
            IWebHostEnvironment webHostingEnvironment,
            ILogger<StorageController> logger)
        {
            _logger = logger;
            _appStorage = appStorage;
            _mapper = mapper;
            _webHostingEnvironment = webHostingEnvironment;
        }

        private readonly ILogger<StorageController> _logger;
        private readonly StorageApp _appStorage;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        /// <summary>
        /// Obter um arquivo de storage
        /// </summary>
        [HttpGet, Route("{Alias}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(byte[]))]
        public IActionResult Get([FromQuery]ObterStorageDataModel values)
        {
            InvocarSeNulo(ref values);

            ObterStorageCmd cmd = _mapper.Map<ObterStorageCmd>(values);
            cmd.ExtrairModelState(ModelState);

            Storage resultado = _appStorage.Obter(cmd);
            Validate(_appStorage);

            return CustomResponseFile(resultado, _webHostingEnvironment);
        }
    }
}
