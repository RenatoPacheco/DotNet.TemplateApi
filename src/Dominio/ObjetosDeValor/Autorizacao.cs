using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Notacoes;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
{
    public class Autorizacao
    {
        protected internal Autorizacao() { }

        public Autorizacao(MethodInfo metodo)
            : this()
        {
            Grupo = metodo.DeclaringType.ToString();
            Acao = metodo.Name;

            Obsoleto = (metodo.GetCustomAttributes(
                typeof(ObsoleteAttribute), true)
                .FirstOrDefault() as ObsoleteAttribute)?.Message?.Trim();

            Nome = (metodo.GetCustomAttributes(
                typeof(DisplayAttribute), true)
                .FirstOrDefault() as DisplayAttribute)?.Name?.Trim() ?? Acao;

            Descricao = (metodo.GetCustomAttributes(
                typeof(DescriptionAttribute), true)
                .FirstOrDefault() as DescriptionAttribute)?.Description?.Trim();

            AcessoLivre = (metodo.GetCustomAttributes(
                typeof(AcessoLivreAttribute), true)
                .FirstOrDefault() != null);

            Id = ToString();
        }

        public string Id { get; set; }

        public string Grupo { get; set; }

        [Display(Name = "Ação")]
        public string Acao { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Acesso livre")]
        public bool AcessoLivre { get; set; }

        public string Obsoleto { get; set; }

        public override string ToString() => $"{Grupo}.{Acao}";
    }
}