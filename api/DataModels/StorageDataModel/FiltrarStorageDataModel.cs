using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel {
    public class FiltrarStorageDataModel
        : Common.FiltrarBaseDataModel<FiltrarStorageDataModel> {

        private EnumInput<ContextoCmd> _contexto;
        /// <summary>
        /// Informe o contexto da busca, sendo que o valor padrão é Embutir
        /// </summary>
        public EnumInput<ContextoCmd> Contexto {
            get => _contexto;
            set {
                _contexto = value;
                this.RemoveAtReference(x => x.Contexto);
                this.InputTypeIsValid(x => x.Contexto);
                RegistrarPropriedade();
            }
        }

        private IList<LongInput> _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<LongInput> Storage {
            get => _storage;
            set {
                _storage = value;
                this.RemoveAtReference(x => x.Storage);
                this.InputTypeIsValid(x => x.Storage);
                RegistrarPropriedade();
            }
        }

        private IList<string> _referencia;
        /// <summary>
        /// Referência de storage
        /// </summary>
        [Display(Name = "Referência")]
        public IList<string> Referencia {
            get => _referencia;
            set {
                _referencia = value;
                RegistrarPropriedade();
            }
        }

        private IList<string> _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        public IList<string> Alias {
            get => _alias;
            set {
                _alias = value;
                RegistrarPropriedade();
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de conteúdo
        /// </summary>
        public IList<EnumInput<Status>> Status {
            get => _status;
            set {
                _status = value;
                this.RemoveAtReference(x => x.Status);
                this.InputTypeIsValid(x => x.Status);
                RegistrarPropriedade();
            }
        }

        public FiltrarStorageCmd Montar() {
            var resultado = new FiltrarStorageCmd();

            if (PropriedadeRegistrada(x => x.Contexto)) {
                if (!this.HasNotification(x => x.Contexto)) {
                    resultado.Contexto = (ContextoCmd?)Contexto;
                }
            }

            if (PropriedadeRegistrada(x => x.Alias)) {
                resultado.Alias = Alias;
            }

            if (PropriedadeRegistrada(x => x.Referencia)) {
                resultado.Referencia = Referencia;
            }

            if (PropriedadeRegistrada(x => x.Storage)) {
                if (!this.HasNotification(x => x.Storage)) {
                    resultado.Storage = Storage.Select(x => (long)x).ToList();
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
