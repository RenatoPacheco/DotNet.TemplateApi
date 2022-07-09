using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Comandos.Comum;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.UsuarioDataModel
{
    public class FiltrarUsuarioDataModel : FiltrarBaseCmd
    {
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status { get; set; }
    }
}
