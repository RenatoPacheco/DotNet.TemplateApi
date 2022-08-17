using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Aplicacao;
using Microsoft.AspNetCore.Mvc.Controllers;
using TemplateApi.Api.ApiServices;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Api.ValuesObject;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Dominio.Comandos.AutenticacaoCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.ApiApplications
{
    public class AutenticacaoApiApp : Common.BaseApiApplications
    {
        public AutenticacaoApiApp(
               AutenticacaoApp appAutenticacao,
               RequestApiServ apiServRequest,
               AutorizacaoApiServ apiServAutorizacao)
        {
            _appAutenticacao = appAutenticacao;
            _apiServRequest = apiServRequest;
            _apiServAutorizacao = apiServAutorizacao;
        }

        private readonly AutenticacaoApp _appAutenticacao;
        private readonly RequestApiServ _apiServRequest;
        private readonly AutorizacaoApiServ _apiServAutorizacao;

        public AutenticacaoApi Autenticacao { get; protected set; }

        public AutenticacaoApi Obter()
        {
            Notifications.Clear();

            Autenticacao core = _appAutenticacao.Obter();
            IsValid(_appAutenticacao);

            Autenticacao = _apiServAutorizacao.Aplicar(core);
            return Autenticacao;
        }

        public Autenticacao ObterCore()
        {
            Notifications.Clear();

            Autenticacao core = _appAutenticacao.Obter();
            IsValid(_appAutenticacao);

            Autenticacao = _apiServAutorizacao.Aplicar(core);
            return core;
        }

        public AutenticacaoApi Iniciar()
        {
            Notifications.Clear();

            Autenticacao origem = _appAutenticacao.Iniciar(new IniciarAutenticacaoCmd
            {
                Token = ExtrairToken(),
                ChavePublica = ExtrairChavePublica(),
            });
            IsValid(_appAutenticacao);

            Autenticacao = _apiServAutorizacao.Aplicar(origem);
            return Autenticacao;
        }

        public bool HaToken() => !string.IsNullOrWhiteSpace(ExtrairToken());

        public bool HaChavePublica() => !string.IsNullOrWhiteSpace(ExtrairChavePublica());

        public bool EstaAutorizado(ControllerActionDescriptor action)
        {
            Autorizacao autorizacao = ExtrairAutorizacao(action);

            AutorizacaoApi[] autorizacoes = Autenticacao?.Autorizacoes ?? Array.Empty<AutorizacaoApi>();

            return autorizacoes.Where(x => x.EstaAutorizado(autorizacao)).FirstOrDefault() != null;
        }

        public Autorizacao ExtrairAutorizacao(ControllerActionDescriptor action)
        {
            ReferenciarAppAttribute atributo = action.MethodInfo.GetCustomAttributes(
                typeof(ReferenciarAppAttribute), true)
                .Select(x => x as ReferenciarAppAttribute).FirstOrDefault();

            return atributo?.ExtrairAutorizacao();
        }

        private string ExtrairToken()
        {
            string resultado = _apiServRequest.GetHeader("Authorization").FirstOrDefault()?.Trim();
            if (!string.IsNullOrWhiteSpace(resultado))
            {
                resultado = resultado.StartsWith("Bearer ") ? resultado[7..] : resultado;
            }
            return resultado;
        }

        private string ExtrairChavePublica()
        {
            return _apiServRequest.GetHeader("Chave-Publica").FirstOrDefault()?.Trim();
        }
    }
}
