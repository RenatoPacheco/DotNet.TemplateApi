using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.DataAnnotations;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.ValuesObject
{
    public class AutorizacaoApi : ICloneable
    {
        public AutorizacaoApi(ApiDescription apiInfo)
        {
            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)apiInfo.ActionDescriptor;
            Metodo = actionDescriptor.MethodInfo;
            Classe = actionDescriptor.ControllerTypeInfo;

            Referencia = ExtrairReferencia(Metodo);
            Http = apiInfo.HttpMethod.ToString();
            Rota = apiInfo.RelativePath;
            ExtrairObsoleto(Metodo);

            Id = Regex.Match(apiInfo.RelativePath, @"^[^?]+").Value;
            Id = Regex.Replace(Id, @"{[^}]+}", "{Param}").ToLower();
            Id = $"{Http.ToLower()}:{Id}";
        }

        public readonly Autorizacao Referencia;

        public readonly MethodInfo Metodo;

        public readonly Type Classe;

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
        public string Nome => Referencia?.Nome;

        /// <summary>
        /// Tipo de requisição do endpoint.
        /// </summary>
        public string Http { get; set; }

        /// <summary>
        /// Uma breve descrição do que o endpoint faz.
        /// </summary>
        [Display(Name = "Descrição")]
        public string Descricao => Referencia?.Descricao;

        /// <summary>
        /// Se true, quer dizer que só pode ser acessado recebendo uma autorização válida.
        /// </summary>
        [Display(Name = "Requer autorização")]
        public bool RequerAutorizacao => Referencia?.RequerAutorizacao ?? true;

        /// <summary>
        /// Se true, quer dizer que só pode ser acessado recebendo uma chave pública válida.
        /// </summary>
        [Display(Name = "Requer chave pública")]
        public bool RequerChavePublica => Referencia?.RequerChavePublica ?? true;

        /// <summary>
        /// Indica se esse endpoint está obsoleto.
        /// </summary>
        public bool Obsoleto { get; set; }

        /// <summary>
        /// Alguma descrição complememntar caso esteja obsoleto.
        /// </summary>
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public bool EstaAutorizado(Autorizacao[] compare)
        {
            return !(Referencia is null) && compare.Any(x => x.Id == Referencia.Id);
        }

        public bool EstaAutorizado(Autorizacao compare)
        {
            return !(Referencia is null) && compare?.Id == Referencia.Id;
        }

        private string ExtrairNome(MethodInfo metodo)
        {
            DisplayAttribute atributo = metodo.GetCustomAttributes(
                typeof(DisplayAttribute), true)
                .FirstOrDefault() as DisplayAttribute;

            return atributo?.Name;
        }        

        private Autorizacao ExtrairReferencia(MethodInfo metodo)
        {
            ReferenciarAppAttribute atributo = metodo.GetCustomAttributes(
                typeof(ReferenciarAppAttribute), true)
                .Select(x => x as ReferenciarAppAttribute).FirstOrDefault();

            return atributo?.ExtrairAutorizacao();
        }

        private void ExtrairObsoleto(MethodInfo metodo)
        {
            ObsoleteAttribute info = metodo.GetCustomAttributes(
                typeof(ObsoleteAttribute), true)
                .FirstOrDefault() as ObsoleteAttribute;

            Obsoleto = !(info is null);
            Observacao = info?.Message;
        }

        public object Clone()
        {
            return (AutorizacaoApi)MemberwiseClone();
        }
    }
}
