using BitHelp.Core.Type.pt_BR;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Compartilhado.Json.Notacoes;

namespace TemplateApi.Api.DataModels.UsuarioDataModel
{
    public class EditarUsuarioDataModel
        : Common.BaseDataModel<EditarUsuarioDataModel>
    {
        private IntInput _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IntInput Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                RegistrarPropriedade();
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                RegistrarPropriedade();
            }
        }

        private string _email;
        /// <summary>
        /// E-mail de usuário
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RegistrarPropriedade();
            }
        }

        private PhoneType? _telefone;
        /// <summary>
        /// Telefone de usuário
        /// </summary>
        public PhoneType? Telefone
        {
            get => _telefone;
            set
            {
                _telefone = value;
                RegistrarPropriedade();
            }
        }

        private string _senha;
        /// <summary>
        /// Senha de usuário
        /// </summary>
        [JsonIgnoreSerialize]
        public string Senha
        {
            get => _senha;
            set
            {
                _senha = value;
                RegistrarPropriedade();
            }
        }


        private EnumInput<Status> _status;
        /// <summary>
        /// Status de conteúdo
        /// </summary>
        public EnumInput<Status> Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistrarPropriedade();
            }
        }
    }
}
