using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class FiltrarUsuarioCmd 
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        /// <summary>
        /// Lista de ids de usuários
        /// </summary>
        [Display(Name = "Usuário")]
        public int[] Usuario { get; set; }

        /// <summary>
        /// Lista de status
        /// </summary>
        public Status[] Status { get; set; }

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
