using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using DotNetCore.API.Template.Site.ValuesObject;
using DotNetCore.API.Template.Dominio.Entidades;

namespace DotNetCore.API.Template.Site.ApiServices
{
    public class AutorizacaoApiServ : Common.baseApiServ
    {
        public AutorizacaoApiServ(
            IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _apiExplorer = apiExplorer;
        }


        private static AutorizacaoApi[] _autorizacoes = null;
        protected readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        private static AutorizacaoApi[] Sincronizar(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            IList<AutorizacaoApi> resultado = new List<AutorizacaoApi>();
            ApiDescriptionGroup[] grupos = apiExplorer.ApiDescriptionGroups.Items.ToArray();

            foreach (ApiDescriptionGroup grupo in grupos)
            {
                ApiDescription[] itens = grupo.Items.ToArray();
                foreach (ApiDescription item in itens)
                {
                    resultado.Add(new AutorizacaoApi(item));
                }
            }

            return resultado.ToArray();
        }

        public AutorizacaoApi[] Autorizacoes
        {
            get => _autorizacoes ??= Sincronizar(_apiExplorer);
        }

        public AutenticacaoApi Aplicar(Autenticacao dados)
        {
            Notifications.Clear();
            AutenticacaoApi resultado = new AutenticacaoApi(dados);
            resultado.Autorizacoes = Autorizacoes.Where(
                x => x.EstaAutorizado(dados.Autorizacoes)).ToArray();

            return resultado;
        }
    }
}
