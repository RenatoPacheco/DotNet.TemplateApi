using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Aplicacao;

namespace DotNetCore.API.Template.Site.ApiServices
{
    public class AutorizacaoApiServ : Common.baseApiServ
    {
        public AutorizacaoApiServ(
            AutorizacaoApp appAutorizacao,
            IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _appAutorizacao = appAutorizacao;
            _apiExplorer = apiExplorer;
        }

        private static AutorizacaoApi[] _autorizacoes = null;
        protected readonly AutorizacaoApp _appAutorizacao;
        protected readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        private static AutorizacaoApi[] Sincronizar(
            IApiDescriptionGroupCollectionProvider apiExplorer,
            AutorizacaoApp appAutorizacao)
        {
            IList<AutorizacaoApi> resultado = new List<AutorizacaoApi>();

            ApiDescriptionGroup[] grupos = apiExplorer.ApiDescriptionGroups.Items.ToArray();
            foreach (ApiDescriptionGroup grupo in grupos)
            {
                ApiDescription[] itens = grupo.Items.ToArray();
                foreach (ApiDescription item in itens)
                {
                    resultado.Add(new AutorizacaoApi(item, appAutorizacao));
                }
            }

            return resultado.ToArray();
        }

        public AutorizacaoApi[] Autorizacoes
        {
            get => _autorizacoes ??= Sincronizar(_apiExplorer, _appAutorizacao);
        }

        public AutenticacaoApi Aplicar(Autenticacao dados)
        {
            Notifications.Clear();
            AutenticacaoApi resultado = new AutenticacaoApi(dados)
            {
                Autorizacoes = Autorizacoes.Where(
                x => x.EstaAutorizado(dados.Autorizacoes)).ToArray()
            };

            return resultado;
        }
    }
}
