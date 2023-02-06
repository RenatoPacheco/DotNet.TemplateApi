using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Dominio.Escopos;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Comandos.ConteudoCmds
{
    public class FiltrarConteudoCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarConteudoCmd()
        {
            _escopo = new ConteudoEscp<FiltrarConteudoCmd>(this);
        }

        private ContextoCmd? _contexto = ContextoCmd.Embutir;
        /// <summary>
        /// Informe o contexto da busca, sendo que o valor padrão é Embutir
        /// </summary>
        public ContextoCmd? Contexto
        {
            get => _contexto;
            set
            {
                _contexto = value;
                this.RemoveAtReference(x => x.Contexto);
            }
        }

        private IList<int> _Conteudo = new List<int>();
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<int> Conteudo
        {
            get => _Conteudo;
            set
            {
                _Conteudo = value ?? new List<int>();
                _escopo.IdEhValido(x => x.Conteudo);
            }
        }

        private IList<Status> _status = new List<Status>();
        /// <summary>
        /// Status de conteúdo
        /// </summary>
        public IList<Status> Status
        {
            get => _status;
            set
            {
                _status = value ?? new List<Status>();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        #region Auto validação

        protected readonly ConteudoEscp<FiltrarConteudoCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            return _notifications.IsValid();
        }

        #endregion
    }
}
