using BitHelp.Core.Validation;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Comandos.ConteudoCmds
{
    public class EditarConteudoCmd 
        : Comum.EditarBaseCmd, ISelfValidation
    {
        public EditarConteudoCmd()
        {
            _escopo = new ConteudoEscp<EditarConteudoCmd>(this);
        }

        private int? _Conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public int? Conteudo
        {
            get => _Conteudo;
            set
            {
                _Conteudo = value;
                RegistrarPropriedade();
                _escopo.IdEhValido(x => x.Conteudo);
            }
        }

        private string _titulo;
        /// <summary>
        /// Título de conteúdo
        /// </summary>
        [Display(Name = "Título")]
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                RegistrarPropriedade();
                _escopo.TituloEhValido(x => x.Titulo);
            }
        }

        private string _alias;
        /// <summary>
        /// Alias de conteúdo
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                _alias = value;
                RegistrarPropriedade();
                _escopo.AliasEhValido(x => x.Alias);
            }
        }

        private string _texto;
        /// <summary>
        /// Texto de conteúdo
        /// </summary>
        public string Texto
        {
            get => _texto;
            set
            {
                _texto = value;
                RegistrarPropriedade();
                _escopo.TextoEhValido(x => x.Texto);
            }
        }

        private Status? _status;
        /// <summary>
        /// Status de conteúdo
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

        public void Aplicar(ref Conteudo dados)
        {
            if (PropriedadeRegistrada(nameof(Titulo)))
            {
                dados.Titulo = Titulo;
            }

            if (PropriedadeRegistrada(nameof(Alias)))
            {
                dados.Alias = Alias;
            }

            if (PropriedadeRegistrada(nameof(Texto)))
            {
                dados.Texto = Texto;
            }

            if (PropriedadeRegistrada(nameof(Status)))
            {
                dados.Status = Status;
            }
        }

        public void Desfazer(ref Conteudo dados) => dados = null;

        #region Auto validação

        protected readonly ConteudoEscp<EditarConteudoCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Conteudo);

            return _notifications.IsValid();
        }

        #endregion
    }
}
