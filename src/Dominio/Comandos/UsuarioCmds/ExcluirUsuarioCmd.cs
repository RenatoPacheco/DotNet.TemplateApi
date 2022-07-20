using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
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
