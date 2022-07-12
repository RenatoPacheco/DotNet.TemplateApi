using System;
using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Entidades
{
    public class Usuario
        : ISelfValidation, IEquatable<Usuario>
    {
        protected Usuario()
        {
            _escopo = new UsuarioEscp<Usuario>(this);
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
            Status = ObjetosDeValor.Status.Inativo;
        }

        public Usuario(string nome, string email, Status? status)
            : this()
        {
            Nome = nome;
            Email = email;
            Status = status;
        }

        public int? Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public string Senha { get; set; }

        public PhoneType? Telefone { get; set; }

        [Display(Name = "Criado em")]
        public DateTime? CriadoEm { get; set; }

        [Display(Name = "AlteradoEm")]
        public DateTime? AlteradoEm { get; set; }

        public Status? Status { get; set; }

        public override string ToString() => Nome;

        #region Compare

        public bool Equals([AllowNull] Usuario other)
        {
            return !(other is null)
                && other.GetHashCode() == GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Usuario other && Equals(other);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}:{Id}".GetHashCode();
        }

        #endregion

        #region Operadores

        public static bool operator ==(Usuario a, Usuario b)
        {
            return (a is null) && (b is null)
                || (!(a is null) && !(b is null) && a.Equals(b));
        }

        public static bool operator !=(Usuario a, Usuario b)
        {
            return !((a is null) && (b is null)
                || (!(a is null) && !(b is null) && a.Equals(b)));
        }

        #endregion

        #region ISelfValidation

        protected UsuarioEscp<Usuario> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; protected set; } = new ValidationNotification();

        public bool IsValid()
        {
            _escopo.IdEhValido(x => x.Id);
            _escopo.NomeEhValido(x => x.Nome);
            _escopo.EmailEhValido(x => x.Email);
            _escopo.TelefoneEhValido(x => x.Telefone);
            _escopo.StatusEhValido(x => x.Status);

            return Notifications.IsValid();
        }

        #endregion
    }
}
