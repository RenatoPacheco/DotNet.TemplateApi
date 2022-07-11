using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetCore.API.Template.Aplicacao;
using Swashbuckle.AspNetCore.Annotations;
using DotNetCore.API.Template.Site.ViewsData;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Site.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using DotNetCore.API.Template.Site.ValuesObject;

namespace DotNetCore.API.Template.Site.Controllers.Servicos
{
    [ApiController, AcessoLivre]
    [Route("Servico/[controller]")]
    public class SobreController : Common.BaseController
    {
        public SobreController(
            SobreApp appSobre,
               IApiDescriptionGroupCollectionProvider apiExplorer,
            ILogger<SobreController> logger)
        {
            _logger = logger;
            _appSobre = appSobre;
            _apiExplorer = apiExplorer;
        }

        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;
        private readonly ILogger<SobreController> _logger;
        private readonly SobreApp _appSobre;

        /// <summary>
        /// Obter informação como nome e versão da API
        /// </summary>
        /// <remarks>
        /// Essa consulta vai permitir obter dados como:
        /// 
        /// <code>
        /// "dados": {
        ///     "nome": "Projeto teste API",
        ///     "versao": "1.0.0",
        ///     "servidor": "local"
        /// } 
        /// </code>
        /// 
        /// </remarks>
        [HttpGet]
        [ReferenciarApp(typeof(SobreApp), nameof(SobreApp.Obter))]
        [SwaggerResponse((int)HttpStatusCode.OK, "", typeof(ComumViewsData<Sobre>))]
        public IActionResult Get()
        {
            ApiDescriptionGroup grupo = _apiExplorer.ApiDescriptionGroups.Items.FirstOrDefault();
            if (!(grupo is null))
            {
                ApiDescription[] itens = grupo.Items.ToArray();
                foreach (ApiDescription item in itens)
                {
                    var a = new AutorizacaoApi(item);
                }
            }

            Sobre resultado = _appSobre.Obter();
            Validate(_appSobre);

            return CustomResponse(resultado);
        }
    }
}
