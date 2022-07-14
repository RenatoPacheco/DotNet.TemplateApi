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

        public Autorizacao(MethodInfo metodo, Type classe)
            : this()
        {
            Classe = classe;
            Metodo = metodo;

            // Analizando classe

            bool naoRequerAutorizacao = (classe.GetCustomAttributes(
                typeof(NaoRequerAutorizacaoAttribute), true)
                .FirstOrDefault() != null);

            bool naoRequerChavePublica = (classe.GetCustomAttributes(
                typeof(NaoRequerChavePublicaAttribute), true)
                .FirstOrDefault() != null);

            bool acessoLivre = (classe.GetCustomAttributes(
                typeof(AcessoLivreAttribute), true)
                .FirstOrDefault() != null);

            // Analizando o método

            naoRequerAutorizacao = (metodo.GetCustomAttributes(
                typeof(NaoRequerAutorizacaoAttribute), true)
                .FirstOrDefault() != null) || naoRequerAutorizacao;

            naoRequerChavePublica = (metodo.GetCustomAttributes(
                typeof(NaoRequerChavePublicaAttribute), true)
                .FirstOrDefault() != null) || naoRequerChavePublica;

            acessoLivre = (metodo.GetCustomAttributes(
                typeof(AcessoLivreAttribute), true)
                .FirstOrDefault() != null) || acessoLivre;

            naoRequerAutorizacao = naoRequerAutorizacao || acessoLivre;
            naoRequerChavePublica = naoRequerChavePublica || acessoLivre;

            // Aplicando resultados

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

            RequerAutorizacao = !naoRequerAutorizacao;

            RequerChavePublica = !naoRequerChavePublica;

            Id = ToString();
        }

        public readonly Type Classe;

        public readonly MethodInfo Metodo;

        public string Id { get; set; }

        public string Grupo { get; set; }

        [Display(Name = "Ação")]
        public string Acao { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Requer autorização")]
        public bool RequerAutorizacao { get; set; } = true;

        [Display(Name = "Requer chave pública")]
        public bool RequerChavePublica { get; set; } = true;

        public string Obsoleto { get; set; }

        public override string ToString() => $"{Grupo}.{Acao}";

        public bool EstaAutorizado(Autenticacao autenticacao)
        {
            return (!RequerAutorizacao || (RequerAutorizacao == autenticacao.EstaAutenticado))
                && (!RequerChavePublica || (RequerChavePublica == autenticacao.HaChavePublica));
        }
    }
}