using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.ConteudoDataModel {

    public class FiltrarConteudoDataModel
        : Common.FiltrarBaseDataModel<FiltrarConteudoDataModel> {

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

        private IList<IntInput> _conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<IntInput> Conteudo {
            get => _conteudo ??= new List<IntInput>();
            set {
                _conteudo = value ?? new List<IntInput>();
                this.RemoveAtReference(x => x.Contexto);
                this.InputTypeIsValid(x => x.Contexto);
                RegistrarPropriedade();
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de conteúdo
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

        public FiltrarConteudoCmd Montar() {

            var resultado = new FiltrarConteudoCmd();

            AplicarBase(resultado);

            if (PropriedadeRegistrada(x => x.Contexto)) {
                if (!this.HasNotification(x => x.Contexto)) {
                    resultado.Contexto = (ContextoCmd?)Contexto;
                }
            }

            if (PropriedadeRegistrada(x => x.Conteudo)) {
                if (!this.HasNotification(x => x.Conteudo)) {
                    resultado.Conteudo = Conteudo.Select(x => (int)x).ToList();
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
