using System;
using System.Linq;
using DotNetCore.API.Template.Aplicacao;
using Microsoft.AspNetCore.Http;
using DotNetCore.API.Template.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc.Controllers;
using DotNetCore.API.Template.Site.ValuesObject;
using System.Reflection;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.AutenticacaoCmds;
using DotNetCore.API.Template.Site.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DotNetCore.API.Template.Site.ApiServices
{
    public class AutenticacaoApiServ : Aplicacao.Comum.BaseApp
    {
        public AutenticacaoApiServ(
               AutenticacaoApp appAutenticacao,
               IHttpContextAccessor httpAccessor,
               IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _appAutenticacao = appAutenticacao;
            _httpAccessor = httpAccessor;
            _apiExplorer = apiExplorer;
        }

        private readonly AutenticacaoApp _appAutenticacao;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        private static AutorizacaoApi[] _opcoes = null;

        private static AutorizacaoApi[] Sincronizar(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            ApiDescriptionGroup grupo = apiExplorer.ApiDescriptionGroups.Items.FirstOrDefault();
            if (!(grupo is null))
            {
                ApiDescription[] itens = grupo.Items.ToArray();
                _opcoes = itens.Select(x => new AutorizacaoApi(x)).ToArray();
            }
            return _opcoes;
        }

        public AutenticacaoApi Autenticacao { get; protected set; }

        public AutenticacaoApi Obter()
        {
            Notifications.Clear();

            Autenticacao = Adaptar(_appAutenticacao.Obter());
            Validate(_appAutenticacao);

            return Autenticacao;
        }

        public AutenticacaoApi Iniciar()
        {
            Notifications.Clear();

            Autenticacao = Adaptar(_appAutenticacao.Iniciar(new IniciarAutenticacaoCmd
            {
                Token = ExtrairToken(),
                ChavePublica = ExtrairChavePublica(),
            }));
            Validate(_appAutenticacao);

            return Autenticacao;
        }

        public bool HaToken() => !string.IsNullOrWhiteSpace(ExtrairToken());

        public bool HaChavePublica() => !string.IsNullOrWhiteSpace(ExtrairChavePublica());

        public bool EstaAutorizado(ControllerActionDescriptor action)
        {
            Requisito requisito = ExtrairRequisito(action.MethodInfo);

            AutorizacaoApi[] autorizacoes = Autenticacao?.Autorizacoes ?? Array.Empty<AutorizacaoApi>();

            bool resultado = !(requisito is null) && autorizacoes.Any(
                x => x.Requisito.Metodo == requisito.Metodo && x.Requisito.Classe == requisito.Classe);

            return resultado;
        }

        private bool ObterChave(string[] fonte, string compare, out string saida)
        {
            saida = fonte.Where(x => string.Equals(x, compare,
                   StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            return !string.IsNullOrWhiteSpace(saida);
        }

        private string ExtrairToken()
        {
            HttpRequest request = _httpAccessor.HttpContext.Request;
            IHeaderDictionary headers = request.Headers;
            string token = null;
            string[] authzHeaders = ObterChave(headers.Keys.ToArray(), "Authorization", out string chave)
                ? headers[chave].ToArray() : Array.Empty<string>();

            if (authzHeaders.Any())
            {
                string bearerToken = authzHeaders.ElementAt(0);
                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            }

            return token;
        }

        private string ExtrairChavePublica()
        {
            HttpRequest request = _httpAccessor.HttpContext.Request;
            IHeaderDictionary headers = request.Headers;
            string chavePublica = null;
            string[] authzHeaders = ObterChave(headers.Keys.ToArray(), "Chave-Publica", out string chave)
                ? headers[chave].ToArray() : Array.Empty<string>();

            if (authzHeaders.Any())
            {
                chavePublica = authzHeaders.ElementAt(0);
            }

            return chavePublica;
        }

        private Requisito ExtrairRequisito(MethodInfo metodo)
        {
            Requisito resultado = null;

            ReferenciarAppAttribute atributo = metodo.GetCustomAttributes(
                typeof(ReferenciarAppAttribute), true)
                .Select(x => x as ReferenciarAppAttribute).FirstOrDefault();

            if (!(atributo is null))
            {
                resultado = new Requisito(atributo.Classe, atributo.Metodo);
            }

            return resultado;
        }

        private AutorizacaoApi[] ExtrairAutorizacoes()
        {
            AutorizacaoApi[] resultado = _opcoes ?? Sincronizar(_apiExplorer);
            return Array.Empty<AutorizacaoApi>().Concat(resultado).ToArray();
        }

        private AutenticacaoApi Adaptar(Autenticacao dados)
        {
            AutenticacaoApi resultado = new AutenticacaoApi(dados);
            resultado.Autorizacoes = ExtrairAutorizacoes()
                        .Where(x => x.EstaAutorizado(dados.Autorizacoes)).ToArray();

            return resultado;
        }
    }
}
