using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds
{
    public class EditarUsuarioCmd 
        : Comum.EditarBaseCmd, ISelfValidation
    {
        public EditarUsuarioCmd()
        {
            _escopo = new UsuarioEscp<EditarUsuarioCmd>(this);
        }

        private IntInputData? _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IntInputData? Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                RegistrarCampo(nameof(Usuario));
                _escopo.IdEhValido(x => x.Usuario);
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                RegistrarCampo(nameof(Nome));
                _escopo.NomeEhValido(x => x.Nome);
            }
        }

        private string _email;
        /// <summary>
        /// E-mail de usuário
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RegistrarCampo(nameof(Email));
                _escopo.EmailEhValido(x => x.Email);
            }
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
                RegistrarCampo(nameof(Telefone));
                _escopo.TelefoneEhValido(x => x.Telefone);
            }
        }

        private EnumInputData<Status>? _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public EnumInputData<Status>? Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistrarCampo(nameof(Status));
                _escopo.StatusEhValido(x => x.Status);
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

            if (CampoFoiRegistrado(nameof(Telefone)))
            {
                dados.Telefone = Telefone;
            }

            if (CampoFoiRegistrado(nameof(Status)))
            {
                dados.Status = Status;
            }
        }

        public void Desfazer(ref Usuario dados) => dados = null;

        #region Auto validação

        protected UsuarioEscp<EditarUsuarioCmd> _escopo;

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
