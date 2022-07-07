using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Validacoes.Extensoes;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class InserirUsuarioCmd : ISelfValidation
    {
        private string _nome;
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Nome
        {
            get => _nome;
            set => _nome = value;
        }

        private string _email;
        /// <summary>
        /// E-mail de usuário
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        private PhoneType? _telefone;
        /// <summary>
        /// Telefone de usuário
        /// </summary>
        public PhoneType? Telefone
        {
            get => _telefone;
            set
            {
                _telefone = value;
                this.RemoveAtReference(x => x.Telefone);
                this.PhoneTypeIsValid(x => x.Telefone);
            }
        }

        private Status? _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status? Status
        {
            get => _status;
            set => _status = value;
        }

        public void Aplicar(ref Usuario dados)
        {
            dados = new Usuario(
                Nome, Email, Status.Value)
            {
                Telefone = Telefone
            };
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
