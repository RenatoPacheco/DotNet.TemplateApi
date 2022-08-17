using BitHelp.Core.Validation;
using BitHelp.Core.Type.pt_BR;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

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

        protected UsuarioEscp<InserirUsuarioCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequerido(x => x.Nome);
            _escopo.EhRequerido(x => x.Email);
            _escopo.EhRequerido(x => x.Senha);
            _escopo.EhRequerido(x => x.Status);

            return Notifications.IsValid();
        }

        #endregion
    }
}
