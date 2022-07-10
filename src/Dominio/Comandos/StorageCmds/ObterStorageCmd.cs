using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.Escopos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Comandos.StorageCmds
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

        private BoolInput _download = (BoolInput)false;
        /// <summary>
        /// Informe true para fazer download do arquivo 
        /// </summary>
        public BoolInput Download
        {
            get => _download;
            set
            {
                _download = value;
                this.RemoveAtReference(x => x.Download);
                this.RequiredIsValid(x => x.Download);
                this.BoolIsValid(x => x.Download);
            }
        }

        #region Auto validação

        protected StorageEscp<ObterStorageCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            this._escopo.EhRequerido(x => x.Alias);
            return Notifications.IsValid();
        }

        #endregion
    }
}
