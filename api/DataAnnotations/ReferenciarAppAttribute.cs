using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Api.ValuesObject;

namespace TemplateApi.Api.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ReferenciarAppAttribute : Attribute
    {
        public ReferenciarAppAttribute(Type classe, string metodo)
        {
            Classe = classe;
            Metodo = classe.GetMethods().Where(x => x.Name == metodo).FirstOrDefault();
        }

        public readonly Type Classe;

        [Display(Name = "Método")]
        public readonly MethodInfo Metodo;

        public Autorizacao ExtrairAutorizacao()
        {
            return new Autorizacao(Metodo, Classe);
        }
    }
}