using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Notacoes;
using DotNetCore.API.Template.Dominio.Entidades;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
{
    public class Autorizacao
    {
        protected internal Autorizacao() { }

        public Autorizacao(MethodInfo metodo, bool acessoLivre, bool acessoBasico)
            : this()
        {
            acessoLivre = (metodo.GetCustomAttributes(
                typeof(AcessoLivreAttribute), true)
                .FirstOrDefault() != null) || acessoLivre;

            acessoBasico = (metodo.GetCustomAttributes(
                typeof(AcessoBasicoAttribute), true)
                .FirstOrDefault() != null) || acessoBasico;

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

            RequerAutenticacao = acessoLivre ? false 
                : acessoBasico ? false : true;

            RequerChavePublica = acessoLivre ? false : true;

            Id = ToString();
        }

        public string Id { get; set; }

        public string Grupo { get; set; }

        [Display(Name = "Ação")]
        public string Acao { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Requer autenticação")]
        public bool RequerAutenticacao { get; set; } = true;

        [Display(Name = "Requer chave pública")]
        public bool RequerChavePublica { get; set; } = true;

        public string Obsoleto { get; set; }

        public override string ToString() => $"{Grupo}.{Acao}";

        public bool EstaAutorizado(Autenticacao autenticacao)
        {
            return (!RequerAutenticacao || (RequerAutenticacao == autenticacao.Autenticado))
                && (!RequerChavePublica || (RequerChavePublica == autenticacao.HaChavePublica));
        }
    }
}