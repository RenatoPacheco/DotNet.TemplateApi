using System;
using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class FiltrarUsuarioCmd 
        : Comum.FiltrarBaseCmd, ISelfValidation
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

        private Status[] _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status[] Status
        {
            get => _status ??= Array.Empty<Status>();
            set => _status = value ?? Array.Empty<Status>();
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
