using System;
using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class ExcluirUsuarioCmd : ISelfValidation
    {
        public ExcluirUsuarioCmd()
        {
            _escopo = new UsuarioEscp<ExcluirUsuarioCmd>(this);
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

        #region Auto validação

        protected UsuarioEscp<ExcluirUsuarioCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequerido(x => x.Usuario);

            return Notifications.IsValid();
        }

        #endregion
    }
}
