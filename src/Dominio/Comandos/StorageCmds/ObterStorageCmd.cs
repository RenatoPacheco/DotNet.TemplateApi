using BitHelp.Core.Validation;
using System.Collections.Generic;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class ObterStorageCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public ObterStorageCmd()
        {
            _escopo = new StorageEscp<ObterStorageCmd>(this);
        }

        private string _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                _alias = value;
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

        private bool? _download = true;
        /// <summary>
        /// Informe true para fazer download do arquivo , valor padrão é true
        /// </summary>
        public bool? Download
        {
            get => _download;
            set
            {
                _download = value;
                this.RemoveAtReference(x => x.Download);
                this.RequiredIsValid(x => x.Download);
            }
        }

        #region Auto validação

        protected readonly StorageEscp<ObterStorageCmd> _escopo;

        private readonly ValidationNotification _notifications = new ValidationNotification();
        ValidationNotification ISelfValidation.Notifications => _notifications;

        public virtual bool IsValid()
        {
            this.RequiredIsValid(x => x.Alias);

            return _notifications.IsValid();
        }

        #endregion
    }
}
