using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TemplateApi.Api.ApiServices;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Api.DataModels.StorageDataModel;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.ViewsData;
using TemplateApi.Aplicacoes;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.Controllers.Services {

    [ApiController]
    [Route("Servico/[controller]")]
    public class StorageController : Common.BaseApiController {
        public StorageController(
            StorageApp appStorage,
            RequestApiServ apiServRequest,
            ILogger<StorageController> logger) {
            _logger = logger;
            _appStorage = appStorage;
            _apiServRequest = apiServRequest;
        }

        private readonly ILogger<StorageController> _logger;
        private readonly StorageApp _appStorage;
        private readonly RequestApiServ _apiServRequest;

        /// <summary>
        /// Filtro de storages
        /// </summary>
        /// <remarks>
        /// <p>Permite fazer uma busca pelos arquivos indexados no banco, mas se não estiver autenticado, só poderá buscar arquivos ativos.</p>
        /// </remarks>
        [HttpGet]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Filtrar))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(BuscaViewData<Storage>))]
        public IActionResult Get([FromQuery] FiltrarStorageDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelStateParaBody(ModelState);

            var cmd = query.Montar();
            var resultado = _appStorage.Filtrar(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Inserir storage
        /// </summary>
        /// <remarks>
        /// <p>Permite enviar arquivos para o storage, desde que se tenha permissão para isso.</p>
        /// <p>Os tipos de arquivos permitidos são:</p>
        /// <ul>
        ///     <li>Arquivos de texto (.txt)</li>
        ///     <li>Documentos (.doc, .docx ou .pdf)</li>
        ///     <li>Planilhas (.xls ou .xslx)</li>
        ///     <li>Imagens (.jpg, .jpeg ou .png)</li>
        /// </ul>
        /// <p>Pode ser enviado mais de um arquivo na requisição, mas todos tem ser válidos para poder gravar no storage.</p>
        /// </remarks>
        [HttpPost]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Inserir))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(BuscaViewData<Storage>))]
        public IActionResult Post([FromForm] InserirStorageDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            var cmd = body.Montar(_apiServRequest);
            var resultado = _appStorage.Inserir(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Editar storage
        /// </summary>
        /// <remarks>
        /// <p>Permite editar dados básicos de um arquivo, como nome e status</p>
        /// </remarks>
        [HttpPatch]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Editar))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<Storage>))]
        public IActionResult Patch([FromBody] EditarStorageDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            var cmd = body.Montar();
            var resultado = _appStorage.Editar(cmd);
            Validate(_appStorage);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Deletar um ou mais storages
        /// </summary>
        /// <remarks>
        /// <p>Permite excluir um ou mais arquivos, que na verdade não são excluídos do servisor, só são alterados para o status de excluído.</p>
        /// </remarks>
        [HttpDelete]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Excluir))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData))]
        public IActionResult Delete([FromQuery] ExcluirStorageDataModel query) {

            InvocarSeNulo(ref query);
            query.ExtrairModelStateParaBody(ModelState);


            var cmd = query.Montar();
            _appStorage.Excluir(cmd);
            Validate(_appStorage);

            return CustomResponse();
        }
    }
}
