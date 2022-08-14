using System.ComponentModel.DataAnnotations;
using BitHelp.Core.Type.pt_BR;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.UsuarioDataModel
{
    public class InserirUsuarioDataModel
    {
        /// <summary>
        /// Nome de usuário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// E-mail de usuário
        /// </summary>
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        /// <summary>
        /// Telefone de usuário
        /// </summary>
        public PhoneType? Telefone { get; set; }

        /// <summary>
        /// Senha de usuário
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public EnumInput<Status> Status { get; set; }
    }
}
