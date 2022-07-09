using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class ExcluirUsuarioCmd : ISelfValidation
    {
        public ExcluirUsuarioCmd()
        {
            _escopo = new UsuarioEscp<ExcluirUsuarioCmd>(this);
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
