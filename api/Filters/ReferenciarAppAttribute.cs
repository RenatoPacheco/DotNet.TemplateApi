using System;

namespace DotNetCore.API.Template.Site.Filters
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
    }
}