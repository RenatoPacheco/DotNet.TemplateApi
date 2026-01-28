using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.UsuarioDataModel {

    public class FiltrarUsuarioDataModel
        : Common.FiltrarBaseDataModel<FiltrarUsuarioDataModel> {

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

        private IList<IntInput> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario {
            get => _usuario ??= new List<IntInput>();
            set {
                _usuario = value ?? new List<IntInput>();
                this.RemoveAtReference(x => x.Usuario);
                this.InputTypeIsValid(x => x.Usuario);
                RegistrarPropriedade();
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de usuário
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

        public FiltrarUsuarioCmd Montar() {

            var resultado = new FiltrarUsuarioCmd();

            AplicarBase(resultado);

            if (PropriedadeRegistrada(x => x.Contexto)) {
                if (!this.HasNotification(x => x.Contexto)) {
                    resultado.Contexto = (ContextoCmd?)Contexto;
                }
            }

            if (PropriedadeRegistrada(x => x.Usuario)) {
                if (!this.HasNotification(x => x.Usuario)) {
                    resultado.Usuario = Usuario.Select(x => (int)x).ToList();
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
