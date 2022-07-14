using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.ConteudoDataModel
{
    public class ExcluirConteudoDataModel
    {
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<IntInput> Conteudo { get; set; }
    }
}
