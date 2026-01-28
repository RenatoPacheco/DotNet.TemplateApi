using System.Net;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Aplicacoes;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.DataModels.StorageDataModel;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Api.DataAnnotations;

namespace TemplateApi.Api.Controllers {

    [ApiController, NaoRequerAutorizacao]
    [Route("[controller]")]
    public class StorageController : Common.BaseApiController {
        public StorageController(
            StorageApp appStorage,
            IWebHostEnvironment webHostingEnvironment,
            ILogger<StorageController> logger) {
            _logger = logger;
            _appStorage = appStorage;
            _webHostingEnvironment = webHostingEnvironment;
        }

        private readonly ILogger<StorageController> _logger;
        private readonly StorageApp _appStorage;
        private readonly IWebHostEnvironment _webHostingEnvironment;

        /// <summary>
        /// Obter um arquivo de storage
        /// </summary>
        /// <remarks>
        /// <p>Permite carregar um arquivo do storage pelo seu alias, mas se não estiver autenticado, só poderá buscar arquivos ativos.</p>
        /// <p>Há opção de visualizar o arquivo, basta indicar donwload para false na query, o padrão é true.</p>
        /// </remarks>
        [HttpGet, Route("{Alias}")]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(byte[]))]
        public IActionResult Get([FromQuery] ObterStorageDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelState(ModelState);

            var cmd = query.Montar();
            var resultado = _appStorage.Obter(cmd);
            Validate(_appStorage);

            // Um exemplo caso precise carregar um arquivo que esteja em bytes
            // byte[] bytes = System.IO.File.ReadAllBytes(resultado.Referencia);
            // return CustomFile(bytes, resultado.Tipo, resultado.Alias, (bool)cmd.Download);

            return CustomPhysicalFile(resultado, _webHostingEnvironment, cmd.Download);
        }
    }
}
