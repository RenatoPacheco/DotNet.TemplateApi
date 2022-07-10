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

        #region Auto validação

        protected StorageEscp<ExcluirStorageCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequeridoSeOutroForNulo(x => x.Storage, y => y.Referencia);
            _escopo.EhRequeridoSeOutroForNulo(x => x.Referencia, y => y.Storage);

            return Notifications.IsValid();
        }

        #endregion
    }
}
