using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Entidades;
using System;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class EditarUsuarioCmd : ISelfValidation
    {
        [Display(Name = "Usuário")]
        public int? Usuario { get; set; }

        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public Status? Status { get; set; }

        public void Aplicar(ref Usuario resultado)
        {
            throw new NotImplementedException();
        }

        #region Auto validação

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            return Notifications.IsValid();
        }

        #endregion
    }
}
