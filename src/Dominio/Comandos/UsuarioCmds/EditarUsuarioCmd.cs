using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Comandos.UsuarioCmds
{
    public class EditarUsuarioCmd 
        : Comum.EditarBaseCmd, ISelfValidation
    {
        public EditarUsuarioCmd()
        {
            _escopo = new UsuarioEscp<EditarUsuarioCmd>(this);
        }

        private int? _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public int? Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                RegistrarPropriedade();
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
                RegistrarPropriedade();
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
                RegistrarPropriedade();
                _escopo.EmailEhValido(x => x.Email);
            }
        }

        private string _senha;
        /// <summary>
        /// Senha de usuário
        /// </summary>
        public string Senha
        {
            get => _senha;
            set
            {
                _senha = value;
                RegistrarPropriedade();
                _escopo.SenhaEhValido(x => x.Senha);
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
                RegistrarPropriedade();
                _escopo.TelefoneEhValido(x => x.Telefone);
            }
        }

        private Status? _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status? Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistrarPropriedade();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        public void Aplicar(ref Usuario dados)
        {
            if (PropriedadeRegistrada(nameof(Nome)))
            {
                dados.Nome = Nome;
            }

            if (PropriedadeRegistrada(nameof(Email)))
            {
                dados.Email = Email;
            }

            if (PropriedadeRegistrada(nameof(Senha)))
            {
                dados.Senha = Senha;
            }

            if (PropriedadeRegistrada(nameof(Telefone)))
            {
                dados.Telefone = Telefone;
            }

            if (PropriedadeRegistrada(nameof(Status)))
            {
                dados.Status = Status;
            }
        }

        public void Desfazer(ref Usuario dados) => dados = null;

        #region Auto validação

        protected readonly UsuarioEscp<EditarUsuarioCmd> _escopo;

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
