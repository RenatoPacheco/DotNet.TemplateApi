using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class EditarUsuarioCmd 
        : Comum.EditarBaseCmd, ISelfValidation
    {
        private int? _usuario;
        [Display(Name = "Usuário")]
        public int? Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                RegistrarCampo(nameof(Usuario));
            }
        }

        private string _nome;
        public string Nome
        {
            get => _nome;
            set 
            { 
                _nome = value;
                RegistrarCampo(nameof(Nome));
            }
        }

        private string _email;
        [Display(Name = "E-mail")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RegistrarCampo(nameof(Email));
            }
        }

        private Status? _status;
        public Status? Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistrarCampo(nameof(Status));
            }
        }

        public void Aplicar(ref Usuario dados)
        {
            if (CampoFoiRegistrado(nameof(Nome)))
            {
                dados.Nome = Nome;
            }

            if (CampoFoiRegistrado(nameof(Email)))
            {
                dados.Email = Email;
            }

            if (CampoFoiRegistrado(nameof(Status)))
            {
                dados.Status = Status;
            }
        }

        #region Auto validação

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Usuario);
            return Notifications.IsValid();
        }

        #endregion
    }
}
