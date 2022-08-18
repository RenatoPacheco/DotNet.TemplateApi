using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;

namespace TemplateApi.Dominio.Comandos.UsuarioCmds
{
    public class ExcluirUsuarioCmd : ISelfValidation
    {
        public ExcluirUsuarioCmd()
        {
            _escopo = new UsuarioEscp<ExcluirUsuarioCmd>(this);
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

        #region Auto validação

        protected readonly UsuarioEscp<ExcluirUsuarioCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Usuario);

            return _notifications.IsValid();
        }

        #endregion
    }
}
