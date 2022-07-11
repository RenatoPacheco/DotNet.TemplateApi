using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class Requisito
    {
        public Requisito(Type classe, string metodo)
        {
            Classe = classe;
            Metodo = metodo;
        }

        public Type Classe { get; protected internal set; }

        [Display(Name = "Método")]
        public string Metodo { get; protected internal set; }
    }
}