using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;
using System.Collections.Generic;
using TemplateApi.Dominio.Escopos;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class ExcluirStorageCmd : ISelfValidation
    {
        public ExcluirStorageCmd()
        {
            _escopo = new StorageEscp<ExcluirStorageCmd>(this);
        }

        private IList<long> _storage = new List<long>();
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<long> Storage
        {
            get => _storage;
            set
            {
                _storage = value ?? new List<long>();
                _escopo.IdEhValido(x => x.Storage);
            }
        }

        private IList<string> _alias = new List<string>();
        /// <summary>
        /// Alias de storage
        /// </summary>
        public IList<string> Alias
        {
            get => _alias;
            set
            {
                _alias = value ?? new List<string>();
                _escopo.AliasEhValido(x => x.Alias);
            }
        }

        #region Auto validação

        protected readonly StorageEscp<ExcluirStorageCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            this.RequiredIfOtherNotNullIsValid(x => x.Storage, Alias);
            this.RequiredIfOtherNotNullIsValid(x => x.Alias, Storage);

            return _notifications.IsValid();
        }

        #endregion
    }
}
