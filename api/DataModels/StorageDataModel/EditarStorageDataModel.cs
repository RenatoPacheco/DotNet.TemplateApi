using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Api.ApiServices;
using TemplateApi.Api.Extensions;

namespace TemplateApi.Api.DataModels.StorageDataModel {

    public class EditarStorageDataModel
        : Common.BaseDataModel<EditarStorageDataModel> {

        private LongInput _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public LongInput Storage {
            get => _storage;
            set {
                _storage = value;
                this.RemoveAtReference(x => x.Storage);
                this.InputTypeIsValid(x => x.Storage);
                RegistrarPropriedade();
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de storage
        /// </summary>
        public string Nome {
            get => _nome;
            set {
                _nome = value;
                RegistrarPropriedade();
            }
        }

        private EnumInput<Status> _status;
        /// <summary>
        /// Status de srorage
        /// </summary>
        public EnumInput<Status> Status {
            get => _status;
            set {
                _status = value;
                this.RemoveAtReference(x => x.Status);
                this.InputTypeIsValid(x => x.Status);
                RegistrarPropriedade();
            }
        }

        public override bool IsValid() {
            return _notifications.IsValid();
        }

        public EditarStorageCmd Montar() {
            var resultado = new EditarStorageCmd();

            if (PropriedadeRegistrada(x => x.Nome)) {
                resultado.Nome = Nome;
            }

            if (PropriedadeRegistrada(x => x.Storage)) {
                if (!this.HasNotification(x => x.Storage)) {
                    resultado.Storage = (long?)Storage;
                }
            }

            if (PropriedadeRegistrada(x => x.Status)) {
                if (!this.HasNotification(x => x.Status)) {
                    resultado.Status = (Status?)Status;
                }
            }

            resultado.AddNotifications(this);

            return resultado;
        }
    }
}
