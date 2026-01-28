using BitHelp.Core.Type.pt_BR;
using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Api.Extensions;
using TemplateApi.Compartilhados.Json.Notacoes;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.UsuarioCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.UsuarioDataModel {
    public class EditarUsuarioDataModel
        : Common.BaseDataModel<EditarUsuarioDataModel> {
        private IntInput _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IntInput Usuario {
            get => _usuario;
            set {
                _usuario = value;
                this.RemoveAtReference(x => x.Usuario);
                this.InputTypeIsValid(x => x.Usuario);
                RegistrarPropriedade();
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Nome {
            get => _nome;
            set {
                _nome = value;
                RegistrarPropriedade();
            }
        }

        private string _email;
        /// <summary>
        /// E-mail de usuário
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email {
            get => _email;
            set {
                _email = value;
                RegistrarPropriedade();
            }
        }

        private PhoneType? _telefone;
        /// <summary>
        /// Telefone de usuário
        /// </summary>
        public PhoneType? Telefone {
            get => _telefone;
            set {
                _telefone = value;
                this.RemoveAtReference(x => x.Telefone);
                this.InputTypeIsValid(x => x.Telefone);
                RegistrarPropriedade();
            }
        }

        private string _senha;
        /// <summary>
        /// Senha de usuário
        /// </summary>
        [JsonIgnoreSerialize]
        public string Senha {
            get => _senha;
            set {
                _senha = value;
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

        public EditarUsuarioCmd Montar() {

            var resultado = new EditarUsuarioCmd();

            if (PropriedadeRegistrada(x => x.Usuario)) {
                if (!this.HasNotification(x => x.Usuario)) {
                    resultado.Usuario = (int?)Usuario;
                }
            }

            if (PropriedadeRegistrada(x => x.Nome)) {
                resultado.Nome = Nome;
            }

            if (PropriedadeRegistrada(x => x.Email)) {
                resultado.Email = Email;
            }

            if (PropriedadeRegistrada(x => x.Telefone)) {
                if (!this.HasNotification(x => x.Telefone)) {
                    resultado.Telefone = Telefone;
                }
            }

            if (PropriedadeRegistrada(x => x.Senha)) {
                resultado.Senha = Senha;
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
