using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using System;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class ExcluirUsuarioCmd : ISelfValidation
    {
        private int[] _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public int[] Usuario
        {
            get => _usuario ??= Array.Empty<int>();
            set => _usuario = value ?? Array.Empty<int>();
        }

        #region Auto validação

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Usuario);
            return Notifications.IsValid();
        }

        #endregion
    }
}
