using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TemplateApi.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Api.ViewsData;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Api.ApiApplications;
using TemplateApi.Api.ValuesObject;

namespace TemplateApi.Api.Controllers.Services
{
    [ApiController, NaoRequerAutorizacao]
    [Route("Servico/[controller]")]
    [ApiExplorerSettings(GroupName = "Autenticação")]
    public class AutenticacaoController : Common.BaseController
    {
        public AutenticacaoController(
            AutenticacaoApiApp apiServAutenticacao,
            ILogger<AutenticacaoController> logger)
        {
            _logger = logger;
            _apiServAutenticacao = apiServAutenticacao;
        }

        private readonly ILogger<AutenticacaoController> _logger;
        private readonly AutenticacaoApiApp _apiServAutenticacao;

        /// <summary>
        /// Obter os dados da autenticação atual
        /// </summary>
        /// <remarks>
        /// <p>Nesse endpoit se tem acesso as rotas disponíveis, mesmo sem estar autenticado, há rotas que você pode ter acesso.
        /// Nesse caso você verá os acessos sobre as seguintes condições:</p>
        /// <ul>
        ///     <li>Não enviar <strong>chave pública</strong> nem a <strong>autorização</strong></li>
        ///     <li>Enviar a <strong>chave pública</strong> mas não enviar a <strong>autorização</strong></li>
        ///     <li>Enviar a <strong>chave pública</strong> e a <strong>autorização</strong></li>
        /// </ul>
        /// <p>Cada um desses tipos de envios vai listar as rotas de acesso de acordo com essas características.
        /// Não existr uma quarta opção de enviar a <strong>autorização</strong>, mas não enviar <strong>chave pública</strong>.
        /// Se não enviar a <strong>chave pública</strong>, a <strong>autorização</strong> será ignorada.</p>
        /// <p>Uma vez listada suas opções de endpoit, taz as informações de requisito para cada uma, podendo ser:</p>
        /// <ul>
        /// <li><strong>requerAutorizacao:</strong> quando indicado true, significa que endpoit só estará acessível, tendo recebido uma <strong>autorização</strong> válida.</li>
        /// <li><strong>requerChavePublica:</strong> quando indicado true, significa que endpoit só estará acessível, tendo recebido uma <strong>chave pública</strong> válida.</li>
        /// </ul>
        /// <p>Lembre-se também que enviar a <strong>autorizações</strong> válida, não garante acesso a todos os endpoint, esse acesso será avaliado de acordo a <strong>autorização</strong> enviada.</p>
        /// </remarks>
        [HttpGet]
        [ReferenciarApp(typeof(AutenticacaoApp), nameof(AutenticacaoApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<AutenticacaoApi>))]
        public IActionResult Get()
        {
            AutenticacaoApi resultado = _apiServAutenticacao.Obter();
            Validate(_apiServAutenticacao);

            return CustomResponse(resultado);
        }

        /// <summary>
        /// Obter os dados da autenticação atual no core da aplicação
        /// </summary>
        /// <remarks>
        /// <p>Esse recurso serve mais como curiosidade sobre as autorizações de acesso.</p>
        /// <p>O dados exibidos aqui, seguem as mesmas regras de <strong>GET /Servico/Autenticacao</strong>, que mostras nas autorizações, as rotas que você tem acesso.</p>
        /// <p>Essas rotas da API são interfaces de acesso, as classe e métodos da aplicação, e nesse endereço, <strong>GET /Servico/Autenticacao/Core</strong>, são as mesmas autorizações, só que no core do site.</p>
        /// </remarks>
        [HttpGet, Route("Core")]
        [ReferenciarApp(typeof(AutenticacaoApp), nameof(AutenticacaoApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewData<AutenticacaoApi>))]
        public IActionResult GetCore()
        {
            Autenticacao resultado = _apiServAutenticacao.ObterCore();
            Validate(_apiServAutenticacao);

            return CustomResponse(resultado);
        }
    }
}
