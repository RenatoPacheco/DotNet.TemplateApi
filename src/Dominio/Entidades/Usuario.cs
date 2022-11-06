using System;
using Newtonsoft.Json;
using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using TemplateApi.Dominio.Escopos;
using System.Diagnostics.CodeAnalysis;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Entidades
{
    public class Usuario
        : ISelfValidation, IEquatable<Usuario>
    {
        [JsonConstructor]
        protected Usuario()
        {
            _escopo = new UsuarioEscp<Usuario>(this);
        }

        public Usuario(string nome, string email, Status? status)
            : this()
        {
            Inicializar();

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

        private void Inicializar()
        {
            CriadoEm = DateTime.Now;
            AlteradoEm = DateTime.Now;
            Status = ObjetosDeValor.Status.Inativo;
        }

        #region Compare

        public bool Equals(Usuario other)
        {
            return !(other is null)
                && other.GetHashCode() == GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is Usuario compare && Equals(compare);
        }

        public override int GetHashCode()
        {
            return $"{GetType()}:{Id}".GetHashCode();
        }

        #endregion

        #region Operadores

        public static bool operator ==(Usuario a, Usuario b)
        {
            return (a is null && b is null) || (a?.Equals(b) ?? false);
        }

        public static bool operator !=(Usuario a, Usuario b)
        {
            return !(a == b);
        }

        #endregion

        #region ISelfValidation

        protected readonly UsuarioEscp<Usuario> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public bool IsValid()
        {
            _escopo.IdEhValido(x => x.Id);
            _escopo.NomeEhValido(x => x.Nome);
            _escopo.EmailEhValido(x => x.Email);
            _escopo.TelefoneEhValido(x => x.Telefone);
            _escopo.StatusEhValido(x => x.Status);

            return _notifications.IsValid();
        }

        #endregion
    }
}
