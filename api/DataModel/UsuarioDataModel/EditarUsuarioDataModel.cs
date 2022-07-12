using System.ComponentModel.DataAnnotations;
using BitHelp.Core.Type.pt_BR;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.UsuarioDataModel
{
    public class EditarUsuarioDataModel
    {
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IntInput? Usuario { get; set; }

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
        public EnumInput<Status>? Status { get; set; }
    }
}
