using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.StorageCmds
{
    public class ExcluirStorageCmd : ISelfValidation
    {
        public ExcluirStorageCmd()
        {
            _escopo = new StorageEscp<ExcluirStorageCmd>(this);
        }

        private IList<LongInput> _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<LongInput> Storage
        {
            get => _storage ??= new List<LongInput>();
            set
            {
                _storage = value ?? new List<LongInput>();
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
