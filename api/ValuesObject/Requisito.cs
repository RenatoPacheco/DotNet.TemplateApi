using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public class Requisito
    {
        public Requisito(Type classe, string metodo)
        {
            Classe = classe;
            Metodo = classe.GetMethods().Where(x => x.Name == metodo).FirstOrDefault();

            Id = $"{classe.Namespace}.{classe.Name}.{Metodo.Name}";

            Acao = Metodo.Name;

            Obsoleto = (Metodo.GetCustomAttributes(
                typeof(ObsoleteAttribute), true)
                .FirstOrDefault() as ObsoleteAttribute)?.Message?.Trim();

            Nome = (Metodo.GetCustomAttributes(
                typeof(DisplayAttribute), true)
                .FirstOrDefault() as DisplayAttribute)?.Name?.Trim() ?? Acao;

            Descricao = (Metodo.GetCustomAttributes(
                typeof(DescriptionAttribute), true)
                .FirstOrDefault() as DescriptionAttribute)?.Description?.Trim();

        }

        public readonly string Id;

        public readonly Type Classe;

        [Display(Name = "Método")]
        public readonly MethodInfo Metodo;

        [Display(Name = "Ação")]
        public readonly string Acao;

        public readonly string Nome;

        [Display(Name = "Descrição")]
        public readonly string Descricao;

        public readonly string Obsoleto;

        public override string ToString() => Id;
    }
}