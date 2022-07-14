using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Comandos.Comum;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.ConteudoDataModel
{
    public class FiltrarConteudoDataModel : FiltrarBaseCmd
    {
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<IntInput> Conteudo { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status { get; set; }
    }
}
