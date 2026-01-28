using BitHelp.Core.Validation.Extends;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.StorageCmds;

namespace TemplateApi.Api.DataModels.StorageDataModel {

    public class ExcluirStorageDataModel
        : Common.BaseDataModel<ExcluirStorageDataModel> {

        private IList<LongInput> _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<LongInput> Storage {
            get => _storage ??= new List<LongInput>();
            set {
                _storage = value ?? new List<LongInput>();
                this.RemoveAtReference(x => x.Storage);
                this.InputTypeIsValid(x => x.Storage);
                RegistrarPropriedade();
            }
        }

        private IList<string> _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        public IList<string> Alias {
            get => _alias ??= new List<string>();
            set {
                _alias = value ?? new List<string>();
                RegistrarPropriedade();
            }
        }

        public ExcluirStorageCmd Montar() {
            var resultado = new ExcluirStorageCmd();

            if (PropriedadeRegistrada(x => x.Alias)) {
                resultado.Alias = Alias;
            }

            if (PropriedadeRegistrada(x => x.Storage)) {
                if (!this.HasNotification(x => x.Storage)) {
                    resultado.Storage = Storage.Select(x => (long)x).ToList();
                }
            }

            resultado.AddNotifications(this);

            return resultado;
        }

        public override bool IsValid() {
            return _notifications.IsValid();
        }
    }
}
