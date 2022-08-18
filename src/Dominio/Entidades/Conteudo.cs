using System;
using BitHelp.Core.Validation;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Entidades
{
    public class Conteudo
        : ISelfValidation, IEquatable<Conteudo>
    {
        protected Conteudo()
        {
            _escopo = new ConteudoEscp<Conteudo>(this);
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
            Status = ObjetosDeValor.Status.Inativo;
        }

        public Conteudo(string titulo, string alias, string texto, Status? status)
            : this()
        {
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

        #region Compare

        public bool Equals([AllowNull] Conteudo other)
        {
            return !(other is null)
                && other.GetHashCode() == GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Conteudo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}:{Id}".GetHashCode();
        }

        #endregion

        #region Operadores

        public static bool operator ==(Conteudo a, Conteudo b)
        {
            return (a is null) && (b is null)
                || (!(a is null) && !(b is null) && a.Equals(b));
        }

        public static bool operator !=(Conteudo a, Conteudo b)
        {
            return !((a is null) && (b is null)
                || (!(a is null) && !(b is null) && a.Equals(b)));
        }

        #endregion

        #region ISelfValidation

        protected ConteudoEscp<Conteudo> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; protected set; } = new ValidationNotification();

        public bool IsValid()
        {
            _escopo.IdEhValido(x => x.Id);
            _escopo.TituloEhValido(x => x.Titulo);
            _escopo.TituloEhValido(x => x.Alias);
            _escopo.TextoEhValido(x => x.Texto);
            _escopo.StatusEhValido(x => x.Status);

            return Notifications.IsValid();
        }

        #endregion
    }
}
