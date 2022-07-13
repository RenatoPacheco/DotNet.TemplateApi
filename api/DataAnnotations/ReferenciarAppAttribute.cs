using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Site.ValuesObject;

namespace DotNetCore.API.Template.Site.DataAnnotations
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