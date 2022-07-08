using System;
using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class FiltrarUsuarioCmd 
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarUsuarioCmd()
        {
            _escopo = new UsuarioEscp<FiltrarUsuarioCmd>(this);
        }

        private IntInputData[] _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IntInputData[] Usuario
        {
            get => _usuario ??= Array.Empty<IntInputData>();
            set
            {
                _usuario = value ?? Array.Empty<IntInputData>();
                _escopo.IdEhValido(x => x.Usuario);
            }
        }

        private EnumInputData<Status>[] _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public EnumInputData<Status>[] Status
        {
            get => _status ??= Array.Empty<EnumInputData<Status>>();
            set
            {
                _status = value ?? Array.Empty<EnumInputData<Status>>();
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
