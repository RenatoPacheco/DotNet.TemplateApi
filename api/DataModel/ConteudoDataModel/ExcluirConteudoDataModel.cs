using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.ConteudoDataModel
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
