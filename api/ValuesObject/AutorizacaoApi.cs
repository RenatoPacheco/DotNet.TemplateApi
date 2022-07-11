using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Site.DataAnnotations;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class AutorizacaoApi
    {
        public AutorizacaoApi(ApiDescription apiInfo)
        {
            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)apiInfo.ActionDescriptor;
            Metodo = actionDescriptor.MethodInfo;
            Classe = actionDescriptor.ControllerTypeInfo;

            Prefixo = actionDescriptor.ControllerName;
            Nome = ExtrairNome(Metodo);
            Requisitos = ExtrairRequisitos(Metodo);
            Verbo = apiInfo.HttpMethod.ToString();
            Rota = apiInfo.RelativePath;
            ExtrairObsoleto(Metodo);

            Id = Regex.Match(apiInfo.RelativePath, @"^[^?]+").Value;
            Id = Regex.Replace(Id, @"{[^}]+}", "{Param}").ToLower();
        }

        public readonly MethodInfo Metodo;

        public readonly Type Classe;

        public readonly Requisito[] Requisitos = new Requisito[] { };

        public string Id { get; set; }

        public string Prefixo { get; set; }

        public string Rota { get; set; }

        public string Nome { get; set; }

        public string Verbo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public bool Obsoleto { get; set; }

        public bool EstaAutorizado(Dominio.ObjetosDeValor.Autorizacao[] compare)
        {
            bool resultado = true;

            foreach (Requisito item in Requisitos)
            {
                if (compare.Where(x => x.Grupo.Equals(item.Classe.FullName)
                     && x.Acao.Equals(item.Metodo)).FirstOrDefault() == null)
                {
                    resultado = false;
                    break;
                }
            }

            return resultado;
        }

        private string ExtrairNome(MethodInfo metodo)
        {
            string resultado = metodo.Name;

            DisplayAttribute atributo = metodo.GetCustomAttributes(
                typeof(DisplayAttribute), true)
                .FirstOrDefault() as DisplayAttribute;

            resultado = atributo?.Name ?? metodo.Name;

            return resultado;
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

        private void ExtrairObsoleto(MethodInfo metodo)
        {
            ObsoleteAttribute info = metodo.GetCustomAttributes(
                typeof(ObsoleteAttribute), true)
                .FirstOrDefault() as ObsoleteAttribute;

            Obsoleto = !(info is null);
            Descricao = info?.Message;
        }
    }
}
