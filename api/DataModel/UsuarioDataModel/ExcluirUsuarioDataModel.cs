using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.UsuarioDataModel
{
    public class ExcluirUsuarioDataModel
    {
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario { get; set; }
    }
}
