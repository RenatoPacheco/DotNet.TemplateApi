using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.StorageCmds
{
    public class FiltrarStorageCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarStorageCmd()
        {
            _escopo = new StorageEscp<FiltrarStorageCmd>(this);
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

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status
        {
            get => _status ??= new List<EnumInput<Status>>();
            set
            {
                _status = value ?? new List<EnumInput<Status>>();
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
