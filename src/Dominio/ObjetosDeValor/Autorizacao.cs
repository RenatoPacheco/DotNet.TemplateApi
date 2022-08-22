using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.ComponentModel;
using TemplateApi.Dominio.Notacoes;
using TemplateApi.Dominio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TemplateApi.Dominio.ObjetosDeValor
{
    public class Autorizacao
        : IEquatable<Autorizacao>
    {
        [JsonConstructor]
        protected Autorizacao() { }

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

        [JsonIgnore]
        public readonly Type Classe;

        [JsonIgnore]
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

        #region Compare

        public bool Equals([AllowNull] Autorizacao other)
        {
            return !(other is null)
                && other.GetHashCode() == GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is Autorizacao compare && Equals(compare);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}:{Id}".GetHashCode();
        }

        #endregion

        #region Operadores

        public static bool operator ==(Autorizacao a, Autorizacao b)
        {
            return (a is null && b is null) || (a?.Equals(b) ?? false);
        }

        public static bool operator !=(Autorizacao a, Autorizacao b)
        {
            return !(a == b);
        }

        #endregion
    }
}