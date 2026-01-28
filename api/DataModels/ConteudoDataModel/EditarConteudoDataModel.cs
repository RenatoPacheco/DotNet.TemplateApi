using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.ConteudoDataModel {
    public class EditarConteudoDataModel
        : Common.BaseDataModel<EditarConteudoDataModel> {
        private IntInput _conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IntInput Conteudo {
            get => _conteudo;
            set {
                _conteudo = value;
                this.RemoveAtReference(x => x.Conteudo);
                this.InputTypeIsValid(x => x.Conteudo);
                RegistrarPropriedade();
            }
        }

        private string _titulo;
        /// <summary>
        /// Título de conteúdo
        /// </summary>
        [Display(Name = "Título")]
        public string Titulo {
            get => _titulo;
            set {
                _titulo = value;
                RegistrarPropriedade();
            }
        }

        private string _alias;
        /// <summary>
        /// Alias de conteúdo
        /// </summary>
        public string Alias {
            get => _alias;
            set {
                _alias = value;
                RegistrarPropriedade();
            }
        }

        private string _texto;
        /// <summary>
        /// Texto de conteúdo
        /// </summary>
        public string Texto {
            get => _texto;
            set {
                _texto = value;
                RegistrarPropriedade();
            }
        }

        private EnumInput<Status> _status;
        /// <summary>
        /// Status de conteúdo
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

        public EditarConteudoCmd Montar() {

            var resultado = new EditarConteudoCmd();

            if (PropriedadeRegistrada(x => x.Conteudo)) {
                if (!this.HasNotification(x => x.Conteudo)) {
                    resultado.Conteudo = (int?)Conteudo;
                }
            }

            if (PropriedadeRegistrada(x => x.Titulo)) {
                resultado.Titulo = Titulo;
            }

            if (PropriedadeRegistrada(x => x.Alias)) {
                resultado.Alias = Alias;
            }

            if (PropriedadeRegistrada(x => x.Texto)) {
                resultado.Texto = Texto;
            }

            if (PropriedadeRegistrada(x => x.Status)) {
                if (!this.HasNotification(x => x.Status)) {
                    resultado.Status = (Status?)Status;
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
