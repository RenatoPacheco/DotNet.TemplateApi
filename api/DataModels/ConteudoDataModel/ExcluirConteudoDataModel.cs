using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.ConteudoCmds;

namespace TemplateApi.Api.DataModels.ConteudoDataModel {

    public class ExcluirConteudoDataModel
        : Common.BaseDataModel<ExcluirConteudoDataModel> {

        private IList<IntInput> _conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<IntInput> Conteudo {
            get => _conteudo ??= new List<IntInput>();
            set {
                _conteudo = value ?? new List<IntInput>();
                this.RemoveAtReference(x => x.Conteudo);
                this.InputTypeIsValid(x => x.Conteudo);
                RegistrarPropriedade();
            }
        }

        public ExcluirConteudoCmd Montar() {

            var resultado = new ExcluirConteudoCmd();

            if (PropriedadeRegistrada(x => x.Conteudo)) {
                if (!this.HasNotification(x => x.Conteudo)) {
                    resultado.Conteudo = Conteudo.Select(x => (int)x).ToList();
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
