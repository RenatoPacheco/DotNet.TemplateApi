using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;
using System.Collections.Generic;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class FiltrarUsuarioCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarUsuarioCmd()
        {
            _escopo = new UsuarioEscp<FiltrarUsuarioCmd>(this);
        }

        private IList<IntInput> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario
        {
            get => _usuario ??= new List<IntInput>();
            set
            {
                _usuario = value ?? new List<IntInput>();
                _escopo.IdEhValido(x => x.Usuario);
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status
        {
            get => _status ??= new List<EnumInput<Status>>();
            set
            {
                _status = value ?? new List<EnumInput<Status>>();
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
