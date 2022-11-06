using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class FiltrarStorageCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarStorageCmd()
        {
            _escopo = new StorageEscp<FiltrarStorageCmd>(this);
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

        private IList<long> _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<long> Storage
        {
            get => _storage ?? (_storage = new List<long>());
            set
            {
                _storage = value ?? new List<long>();
                _escopo.IdEhValido(x => x.Storage);
            }
        }

        private IList<string> _referencia;
        /// <summary>
        /// Referência de storage
        /// </summary>
        [Display(Name = "Referência")]
        public IList<string> Referencia
        {
            get => _referencia ?? (_referencia = new List<string>());
            set
            {
                _referencia = value ?? new List<string>();
                _escopo.ReferenciaEhValido(x => x.Referencia);
            }
        }

        private IList<string> _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        public IList<string> Alias
        {
            get => _alias ?? (_alias = new List<string>());
            set
            {
                _alias = value ?? new List<string>();
                _escopo.AliasEhValido(x => x.Alias);
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

        protected readonly StorageEscp<FiltrarStorageCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            return _notifications.IsValid();
        }

        #endregion
    }
}
