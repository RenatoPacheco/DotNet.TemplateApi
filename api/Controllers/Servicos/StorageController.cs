using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemplateApi.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.Extensions;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Api.DataModel.StorageDataModel;
using TemplateApi.Api.DataAnnotations;

namespace TemplateApi.Api.Controllers.Servicos
{
    [ApiController]
    [Route("Servico/[controller]")]
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
        /// <remarks>
        /// <p>Permite fazer uma busca pelos arquivos indexados no banco, mas se não estiver autenticado, só poderá buscar arquivos ativos.</p>
        /// </remarks>
        [HttpGet]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Filtrar))]
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
        /// <remarks>
        /// <p>Permite enviar arquivos para o storage, desde que se tenha permissão para isso.</p>
        /// <p>Os tipos de arquivos permitidos são:</p>
        /// <ul>
        ///     <li>Arquivos de texto (.txt)</li>
        ///     <li>Documentos (.doc ou .docx, .pdf)</li>
        ///     <li>Planilhas (.xls ou .xslx)</li>
        ///     <li>Imagens (.jpg, .jpeg ou .png)</li>
        /// </ul>
        /// <p>Pode ser enviado mais de um arquivo na requisição, mas todos tem ser válidos para poder gravar no storage.</p>
        /// </remarks>
        [HttpPost]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Inserir))]
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
        /// <remarks>
        /// <p>Permite editar dados básicos de um arquivo, como nome e status</p>
        /// </remarks>
        [HttpPatch]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Editar))]
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
        /// <remarks>
        /// <p>Permite excluir um ou mais arquivos, que na verdade não são excluídos do servisor, só são alterados para o status de excluído.</p>
        /// </remarks>
        [HttpDelete]
        [ReferenciarApp(typeof(StorageApp), nameof(StorageApp.Excluir))]
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
