using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Site.Filters;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.ViewsData;
using System.Threading.Tasks;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Site.Extensions;
using DotNetCore.API.Template.Site.DataModel.StorageDataModel;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Aplicacao;
using AutoMapper;

namespace DotNetCore.API.Template.Site.Controllers
{
    [ApiController, AcessoLivre]
    [Route("servico/[controller]")]
    public class StorageController : Common.BaseController
    {
        public StorageController(
            StorageApp appStorage,
            IMapper mapper,
            ILogger<UsuarioController> logger)
        {
            _logger = logger;
            _appStorage = appStorage;
            _mapper = mapper;
        }

        private readonly ILogger<UsuarioController> _logger;
        private readonly StorageApp _appStorage;
        private readonly IMapper _mapper;
        /// <summary>
        /// Inserir arquivos no storage
        /// </summary>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Arquivo[]>))]
        public IActionResult Post([FromForm] InserirStorageDataModel form)
        {
            InvocarSeNulo(ref form);

            InserirStorageCmd cmd = _mapper.Map<InserirStorageCmd>(form);
            cmd.ExtrairModelStateParaBody(ModelState);

            Arquivo[] resultado = _appStorage.Inserir(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }
    }
}
