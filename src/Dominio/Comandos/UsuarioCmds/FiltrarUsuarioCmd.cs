using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
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

        private IList<int> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<int> Usuario
        {
            get => _usuario ??= new List<int>();
            set
            {
                _usuario = value ?? new List<int>();
                _escopo.IdEhValido(x => x.Usuario);
            }
        }

        private IList<Status> _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<Status> Status
        {
            get => _status ??= new List<Status>();
            set
            {
                _status = value ?? new List<Status>();
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
