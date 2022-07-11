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

namespace DotNetCore.API.Template.Site.ApiServices
{
    public class AutenticacaoApiServ : Aplicacao.Comum.BaseApp
    {
        public AutenticacaoApiServ(
               AutenticacaoApp appAutenticacao,
               IHttpContextAccessor httpAccessor)
        {
            _appAutenticacao = appAutenticacao;
            _httpAccessor = httpAccessor;
        }

        private readonly AutenticacaoApp _appAutenticacao;
        private readonly IHttpContextAccessor _httpAccessor;

        public Autenticacao Autenticacao { get; protected set; }

        public Autenticacao Obter()
        {
            Notifications.Clear();

            Autenticacao = _appAutenticacao.Obter();
            Validate(_appAutenticacao);

            return Autenticacao;
        }

        public Autenticacao Iniciar()
        {
            Notifications.Clear();

            Autenticacao = _appAutenticacao.Iniciar(new IniciarAutenticacaoCmd
            {
                Token = ExtrairToken(),
                ChavePublica = ExtrairChavePublica(),
            });
            Validate(_appAutenticacao);

            return Autenticacao;
        }

        public bool HaToken() => !string.IsNullOrWhiteSpace(ExtrairToken());

        public bool HaChavePublica() => !string.IsNullOrWhiteSpace(ExtrairChavePublica());

        public bool EstaAutorizado(ControllerActionDescriptor action)
        {
            Requisito requisito = ExtrairRequisitos(action.MethodInfo).FirstOrDefault();

            Autorizacao[] autorizacoes = Autenticacao?.Autorizacoes ?? Array.Empty<Autorizacao>();

            bool resultado = !(requisito is null) && autorizacoes.Any(
                x => x.Acao == requisito.Metodo && x.Grupo == requisito.Classe.FullName);

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

        private Requisito[] ExtrairRequisitos(MethodInfo metodo)
        {
            IList<Requisito> resultado = new List<Requisito>();

            IEnumerable<ReferenciarAppAttribute> atributo = metodo.GetCustomAttributes(
                typeof(ReferenciarAppAttribute), true)
                .Select(x => x as ReferenciarAppAttribute);

            foreach (ReferenciarAppAttribute c in atributo)
            {
                resultado.Add(new Requisito(c.Classe, c.Metodo));
            }

            return resultado.ToArray();
        }
    }
}
