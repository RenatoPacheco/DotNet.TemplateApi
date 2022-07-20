using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class FiltrarStorageCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarStorageCmd()
        {
            _escopo = new StorageEscp<FiltrarStorageCmd>(this);
        }

        private IList<long> _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<long> Storage
        {
            get => _storage ??= new List<long>();
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
            get => _referencia ??= new List<string>();
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
            get => _alias ??= new List<string>();
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
            get => _status ??= new List<Status>();
            set
            {
                _status = value ?? new List<Status>();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        #region Auto validação

        protected StorageEscp<FiltrarStorageCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            return Notifications.IsValid();
        }

        #endregion
    }
}
