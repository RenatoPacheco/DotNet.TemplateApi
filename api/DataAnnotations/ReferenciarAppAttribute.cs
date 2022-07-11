using System;
using DotNetCore.API.Template.Site.ValuesObject;

namespace DotNetCore.API.Template.Site.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ReferenciarAppAttribute : Attribute
    {
        public ReferenciarAppAttribute(Type classe, string metodo)
        {
            Classe = classe;
            Metodo = metodo;
        }

        public Type Classe { get; private set; }

        public string Metodo { get; private set; }

        public Requisito ExtrairRequisito()
        {
            return new Requisito(Classe, Metodo);
        }
    }
}