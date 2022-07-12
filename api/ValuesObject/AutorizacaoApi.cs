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
            
            Prefixo = actionDescriptor.ControllerName;
            Requisito = ExtrairRequisito(Metodo);
            Nome = Requisito?.Nome;
            Descricao = Requisito?.Descricao;
            Verbo = apiInfo.HttpMethod.ToString();
            Rota = apiInfo.RelativePath;
            ExtrairObsoleto(Metodo);

            Id = Regex.Match(apiInfo.RelativePath, @"^[^?]+").Value;
            Id = Regex.Replace(Id, @"{[^}]+}", "{Param}").ToLower();

            Referencia = appAutorizacao.Listar().Where(x => x.Id == Requisito?.Id).FirstOrDefault();
        }

        public readonly Autorizacao Referencia;

        public readonly MethodInfo Metodo;

        public readonly Type Classe;

        public readonly Requisito Requisito = null;

        public string Id { get; set; }

        public string Prefixo { get; set; }

        public string Rota { get; set; }

        public string Nome { get; set; }

        public string Verbo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Requer autenticação")]
        public bool? RequerAutenticacao => Referencia?.RequerAutenticacao;

        [Display(Name = "Requer chave pública")]
        public bool? RequerChavePublica => Referencia?.RequerChavePublica;

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
