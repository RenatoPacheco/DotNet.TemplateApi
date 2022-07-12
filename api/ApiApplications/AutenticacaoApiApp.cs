using System;
using System.Linq;
using System.Reflection;
using DotNetCore.API.Template.Aplicacao;
using Microsoft.AspNetCore.Mvc.Controllers;
using DotNetCore.API.Template.Site.ApiServices;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Site.DataAnnotations;
using DotNetCore.API.Template.Dominio.Comandos.AutenticacaoCmds;

namespace DotNetCore.API.Template.Site.ApiApplications
{
    public class AutenticacaoApiApp : Common.BaseApiApp
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

            Autenticacao origem = _appAutenticacao.Obter();
            Validate(_appAutenticacao);

            Autenticacao = _apiServAutorizacao.Aplicar(origem);
            return Autenticacao;
        }

        public AutenticacaoApi Iniciar()
        {
            Notifications.Clear();

            Autenticacao origem = _appAutenticacao.Iniciar(new IniciarAutenticacaoCmd
            {
                Token = ExtrairToken(),
                ChavePublica = ExtrairChavePublica(),
            });
            Validate(_appAutenticacao);

            Autenticacao = _apiServAutorizacao.Aplicar(origem);
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
    }
}
