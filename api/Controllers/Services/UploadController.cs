using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TemplateApi.Api.ApiServices;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Api.DataModels.UploadDataModel;
using TemplateApi.Api.Extensions;
using TemplateApi.Api.Filters;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.ViewsData.CKEditorViewData;
using TemplateApi.Aplicacoes;
using TemplateApi.Dominio.Interfaces;

namespace TemplateApi.Api.Controllers.Services {

    [ApiController]
    [Route("Servico/[controller]")]
    public class UploadController : Common.BaseApiController {
        public UploadController(
            RequestApiServ apiServRequest,
            UploadApp appUpload,
            ILogger<UploadController> logger) {
            _logger = logger;
            _appUpload = appUpload;
            _apiServRequest = apiServRequest;
        }

        private readonly UploadApp _appUpload;
        private readonly ILogger<UploadController> _logger;
        private readonly RequestApiServ _apiServRequest;

        /// <summary>
        /// Upload de arquivos para links
        /// </summary>
        /// <remarks>
        /// <p>Permite enviar arquivos para o storage público, desde que se tenha permissão para isso.</p>
        /// <p>Os tipos de arquivos permitidos são:</p>
        /// <ul>
        ///     <li>Arquivos de texto (.txt)</li>
        ///     <li>Documentos (.doc, .docx ou .pdf)</li>
        ///     <li>Planilhas (.xls ou .xslx)</li>
        ///     <li>Imagens (.jpg, .jpeg ou .png)</li>
        /// </ul>
        /// <p>Pode ser enviado mais de um arquivo na requisição, mas todos tem ser válidos para poder gravar no storage.</p>
        /// </remarks>
        [HttpPost, Route("Arquivo")]
        [ReferenciarApp(typeof(UploadApp), nameof(UploadApp.Arquivo))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<IArquivo[]>))]
        public IActionResult PostArquivo([FromForm] ArquivoUploadDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            var cmd = body.Montar(_apiServRequest);
            var resultado = _appUpload.Arquivo(cmd);
            Validate(_appUpload);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Upload de arquivos para imagens
        /// </summary>
        /// <remarks>
        /// <p>Permite enviar arquivos para o storage público, desde que se tenha permissão para isso.</p>
        /// <p>Os tipos de arquivos permitidos são:</p>
        /// <ul>
        ///     <li>Imagens (.jpg, .jpeg ou .png)</li>
        /// </ul>
        /// <p>Pode ser enviado mais de um arquivo na requisição, mas todos tem ser válidos para poder gravar no storage.</p>
        /// </remarks>
        [HttpPost, Route("Imagem")]
        [ReferenciarApp(typeof(UploadApp), nameof(UploadApp.Imagem))]
        [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(ComumViewData<IArquivo[]>))]
        public IActionResult PostImagem([FromForm] ImagemUploadDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            var cmd = body.Montar(_apiServRequest);
            var resultado = _appUpload.Imagem(cmd);
            Validate(_appUpload);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Upload de arquivos para links do CKEditor 4 
        /// </summary>
        /// <remarks>
        /// <p>Permite enviar arquivos para o storage público, desde que se tenha permissão para isso.</p>
        /// <p>Os tipos de arquivos permitidos são:</p>
        /// <ul>
        ///     <li>Arquivos de texto (.txt)</li>
        ///     <li>Documentos (.doc, .docx ou .pdf)</li>
        ///     <li>Planilhas (.xls ou .xslx)</li>
        ///     <li>Imagens (.jpg, .jpeg ou .png)</li>
        /// </ul>
        /// </remarks>
        [HttpPost, Route("CKEditor/V4/Arquivo"), IgnorarFiltroAutorizacao]
        [ReferenciarApp(typeof(UploadApp), nameof(UploadApp.Arquivo))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(V4CKEditorViewData))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(V4CKEditorViewData))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "", typeof(V4CKEditorViewData))]
        public IActionResult PostCkEditorArquivo([FromForm] ArquivoCKEditorV4UploadDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            var cmd = body.Montar(_apiServRequest);
            var resultado = _appUpload.Arquivo(cmd);
            Validate(_appUpload);

            return CKEditorV4Response(resultado.FirstOrDefault());
        }

        /// <summary>
        /// Upload de arquivos para imagens do CKEditor 4 
        /// </summary>
        /// <remarks>
        /// <p>Permite enviar arquivos para o storage público, desde que se tenha permissão para isso.</p>
        /// <p>Os tipos de arquivos permitidos são:</p>
        /// <ul>
        ///     <li>Imagens (.jpg, .jpeg ou .png)</li>
        /// </ul>
        /// </remarks>
        [HttpPost, Route("CKEditor/V4/Imagem"), IgnorarFiltroAutorizacao]
        [ReferenciarApp(typeof(UploadApp), nameof(UploadApp.Imagem))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(V4CKEditorViewData))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(V4CKEditorViewData))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "", typeof(V4CKEditorViewData))]
        public IActionResult PostCkEditorImagem([FromForm] ImagemCKEditorV4UploadDataModel body) {

            InvocarSeNulo(ref body);
            body.ExtrairModelStateParaBody(ModelState);

            var cmd = body.Montar(_apiServRequest);
            var resultado = _appUpload.Imagem(cmd);
            Validate(_appUpload);

            return CKEditorV4Response(resultado.FirstOrDefault());
        }
    }
}
