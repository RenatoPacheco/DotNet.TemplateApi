using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Api.DataModels.UsuarioDataModel {
    public class ExcluirUsuarioDataModel
        : Common.BaseDataModel<ExcluirUsuarioDataModel> {
        private IList<IntInput> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario {
            get => _usuario;
            set {
                _usuario = value;
                this.RemoveAtReference(x => x.Usuario);
                this.InputTypeIsValid(x => x.Usuario);
                RegistrarPropriedade();
            }
        }

        public ExcluirUsuarioCmd Montar() {

            var resultado = new ExcluirUsuarioCmd();

            if (PropriedadeRegistrada(x => x.Usuario)) {
                if (!this.HasNotification(x => x.Usuario)) {
                    resultado.Usuario = Usuario.Select(x => (int)x).ToList();
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
