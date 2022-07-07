using System;
using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class FiltrarUsuarioCmd 
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarUsuarioCmd()
        {
            _escopo = new UsuarioEscp<FiltrarUsuarioCmd>(this);
        }

        private int[] _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public int[] Usuario
        {
            get => _usuario ??= Array.Empty<int>();
            set
            {
                _usuario = value ?? Array.Empty<int>();
                _escopo.IdEhValido(x => x.Usuario);
            }
        }

        private Status[] _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status[] Status
        {
            get => _status ??= Array.Empty<Status>();
            set
            {
                _status = value ?? Array.Empty<Status>();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        #region Auto validação

        protected UsuarioEscp<FiltrarUsuarioCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            return Notifications.IsValid();
        }

        #endregion
    }
}
