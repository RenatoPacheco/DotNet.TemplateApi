using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.ConteudoDataModel
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
