using BitHelp.Core.Validation.Extends;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel {

    public class ObterStorageDataModel
        : Common.FiltrarBaseDataModel<ObterStorageDataModel> {

        private string _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        [FromRoute]
        public string Alias {
            get => _alias;
            set {
                _alias = value;
                RegistrarPropriedade();
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de srorage
        /// </summary>
        public IList<EnumInput<Status>> Status {
            get => _status ??= new List<EnumInput<Status>>();
            set {
                _status = value ?? new List<EnumInput<Status>>();
                this.RemoveAtReference(x => x.Status);
                this.InputTypeIsValid(x => x.Status);
                RegistrarPropriedade();
            }
        }

        private BoolInput _download;
        /// <summary>
        /// Informe true para fazer download do arquivo 
        /// </summary>
        public BoolInput Download {
            get => _download;
            set {
                _download = value;
                this.RemoveAtReference(x => x.Download);
                this.InputTypeIsValid(x => x.Download);
                RegistrarPropriedade();
            }
        }

        public ObterStorageCmd Montar() {
            var resultado = new ObterStorageCmd();

            if (PropriedadeRegistrada(x => x.Alias)) {
                resultado.Alias = Alias;
            }

            if (PropriedadeRegistrada(x => x.Download)) {
                if (!this.HasNotification(x => x.Download)) {
                    resultado.Download = (bool?)Download;
                }
            }

            if (PropriedadeRegistrada(x => x.Status)) {
                if (!this.HasNotification(x => x.Status)) {
                    resultado.Status = Status.Select(x => (Status)x).ToList();
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

