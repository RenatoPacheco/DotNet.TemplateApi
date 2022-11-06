using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Comandos.ConteudoCmds
{
    public class ExcluirConteudoCmd : ISelfValidation
    {
        public ExcluirConteudoCmd()
        {
            _escopo = new ConteudoEscp<ExcluirConteudoCmd>(this);
        }

        private IList<int> _conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<int> Conteudo
        {
            get => _conteudo ?? (_conteudo = new List<int>());
            set
            {
                _conteudo = value ?? new List<int>();
                _escopo.IdEhValido(x => x.Conteudo);
            }
        }

        #region Auto validação

        protected readonly ConteudoEscp<ExcluirConteudoCmd> _escopo;

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
