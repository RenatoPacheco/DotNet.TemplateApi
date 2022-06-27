using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class ExcluirUsuarioCmd : ISelfValidation
    {
        [Display(Name = "Usuário")]
        public int[] Usuario { get; set; }

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
