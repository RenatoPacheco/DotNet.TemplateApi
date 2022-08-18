using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Comandos.UsuarioCmds
{
    public class InserirUsuarioCmd : ISelfValidation
    {
        public InserirUsuarioCmd()
        {
            _escopo = new UsuarioEscp<InserirUsuarioCmd>(this);
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
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        public void Aplicar(ref Usuario dados)
        {
            dados = new Usuario(
                Nome, Email, Status)
            {
                Senha = Senha,
                Telefone = Telefone
            };
        }

        public void Desfazer(ref Usuario dados) => dados = null;

        #region Auto validação

        protected readonly UsuarioEscp<InserirUsuarioCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Nome);
            this.RequiredIsValid(x => x.Email);
            this.RequiredIsValid(x => x.Senha);
            this.RequiredIsValid(x => x.Status);

            return _notifications.IsValid();
        }

        #endregion
    }
}
