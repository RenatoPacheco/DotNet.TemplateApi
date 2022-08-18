using BitHelp.Core.Validation;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.StorageCmds
{
    public class ExcluirStorageCmd : ISelfValidation
    {
        public ExcluirStorageCmd()
        {
            _escopo = new StorageEscp<ExcluirStorageCmd>(this);
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

        #region Auto validação

        protected StorageEscp<ExcluirStorageCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequeridoSeOutroForNulo(x => x.Storage, y => y.Alias);
            _escopo.EhRequeridoSeOutroForNulo(x => x.Alias, y => y.Storage);

            return Notifications.IsValid();
        }

        #endregion
    }
}
