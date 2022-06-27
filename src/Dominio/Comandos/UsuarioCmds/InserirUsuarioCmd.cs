using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class InserirUsuarioCmd : ISelfValidation
    {
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public Status? Status { get; set; }

        public void Aplicar(ref Usuario dados)
        {
            dados = new Usuario(
                Nome, Email, Status.Value);
        }

        public void Desfazer(ref Usuario dados) => dados = null;

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
