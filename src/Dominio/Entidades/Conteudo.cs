using System;
using Newtonsoft.Json;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Entidades
{
    public class Conteudo
        : ISelfValidation, IEquatable<Conteudo>
    {
        [JsonConstructor]
        protected Conteudo()
        {
            _escopo = new ConteudoEscp<Conteudo>(this);
        }

        public Conteudo(string titulo, string alias, string texto, Status? status)
            : this()
        {
            Inicializar();

            Titulo = titulo;
            Texto = texto;
            Alias = alias;
            Status = status;
        }

        public int? Id { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        public string Alias { get; set; }

        public string Texto { get; set; }

        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        [Display(Name = "AlteradoEm")]
        public DateTime? AlteradoEm { get; set; }

        public Status? Status { get; set; }

        public override string ToString() => Titulo;

        private void Inicializar()
        {
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
            Status = ObjetosDeValor.Status.Inativo;
        }

        #region Compare

        public bool Equals(Conteudo other)
        {
            return !(other is null)
                && other.GetHashCode() == GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is Conteudo compare && Equals(compare);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}:{Id}".GetHashCode();
        }

        #endregion

        #region Operadores

        public static bool operator ==(Conteudo a, Conteudo b)
        {
            return (a is null && b is null) || (a?.Equals(b) ?? false);
        }

        public static bool operator !=(Conteudo a, Conteudo b)
        {
            return !(a == b);
        }

        #endregion

        #region ISelfValidation

        protected readonly ConteudoEscp<Conteudo> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        bool ISelfValidation.IsValid()
        {
            _escopo.IdEhValido(x => x.Id);
            _escopo.TituloEhValido(x => x.Titulo);
            _escopo.TituloEhValido(x => x.Alias);
            _escopo.TextoEhValido(x => x.Texto);
            _escopo.StatusEhValido(x => x.Status);

            return _notifications.IsValid();
        }

        #endregion
    }
}
