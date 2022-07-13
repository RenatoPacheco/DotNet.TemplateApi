using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Site.DataAnnotations;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Aplicacao;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class AutorizacaoApi : ICloneable
    {
        public AutorizacaoApi(ApiDescription apiInfo, AutorizacaoApp appAutorizacao)
        {
            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)apiInfo.ActionDescriptor;
            Metodo = actionDescriptor.MethodInfo;
            Classe = actionDescriptor.ControllerTypeInfo;
            
            Requisito = ExtrairRequisito(Metodo);
            Nome = Requisito?.Nome;
            Descricao = Requisito?.Descricao;
            Http = apiInfo.HttpMethod.ToString();
            Rota = apiInfo.RelativePath;
            ExtrairObsoleto(Metodo);

            Id = Regex.Match(apiInfo.RelativePath, @"^[^?]+").Value;
            Id = Regex.Replace(Id, @"{[^}]+}", "{Param}").ToLower();
            Id = $"{Http.ToLower()}:{Id}";

            Referencia = appAutorizacao.Listar().Where(x => x.Id == Requisito?.Id).FirstOrDefault();
        }

        public readonly Autorizacao Referencia;

        public readonly MethodInfo Metodo;

        public readonly Type Classe;

        public readonly Requisito Requisito = null;

        /// <summary>
        /// Identificador únido da autorização.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Endpoint referente a essa autorização.
        /// </summary>
        public string Rota { get; set; }

        /// <summary>
        /// Nome da autorirização.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de requisição do endpoint.
        /// </summary>
        public string Http { get; set; }

        /// <summary>
        /// Uma breve descrição do que o endpoint faz.
        /// </summary>
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        /// <summary>
        /// Se true, quer dizer que só pode ser acessado recebendo uma autorização válida.
        /// </summary>
        [Display(Name = "Requer autenticação")]
        public bool? RequerAutenticacao => Referencia?.RequerAutenticacao;

        /// <summary>
        /// Se true, quer dizer que só pode ser acessado recebendo uma chave pública válida.
        /// </summary>
        [Display(Name = "Requer chave pública")]
        public bool? RequerChavePublica => Referencia?.RequerChavePublica;

        /// <summary>
        /// Indica se esse endpoint está obsoleto.
        /// </summary>
        public bool Obsoleto { get; set; }

        public bool EstaAutorizado(Autorizacao[] compare)
        {
            return !(Requisito is null) && compare.Any(x => x.Id == Requisito.Id);
        }

        private string ExtrairNome(MethodInfo metodo)
        {
            DisplayAttribute atributo = metodo.GetCustomAttributes(
                typeof(DisplayAttribute), true)
                .FirstOrDefault() as DisplayAttribute;

            return atributo?.Name;
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

        private void ExtrairObsoleto(MethodInfo metodo)
        {
            ObsoleteAttribute info = metodo.GetCustomAttributes(
                typeof(ObsoleteAttribute), true)
                .FirstOrDefault() as ObsoleteAttribute;

            Obsoleto = !(info is null);
            Descricao = info?.Message ?? Descricao;
        }

        public object Clone()
        {
            return (AutorizacaoApi)MemberwiseClone();
        }
    }
}
