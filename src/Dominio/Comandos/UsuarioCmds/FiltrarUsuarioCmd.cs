using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Dominio.Escopos;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Comandos.UsuarioCmds
{
    public class FiltrarUsuarioCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarUsuarioCmd()
        {
            _escopo = new UsuarioEscp<FiltrarUsuarioCmd>(this);
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

        private IList<int> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<int> Usuario
        {
            get => _usuario ?? (_usuario = new List<int>());
            set
            {
                _usuario = value ?? new List<int>();
                _escopo.IdEhValido(x => x.Usuario);
            }
        }

        private IList<Status> _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<Status> Status
        {
            get => _status ?? (_status = new List<Status>());
            set
            {
                _status = value ?? new List<Status>();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        #region Auto validação

        protected readonly UsuarioEscp<FiltrarUsuarioCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            return _notifications.IsValid();
        }

        #endregion
    }
}
