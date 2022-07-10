using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.StorageDataModel
{
    public class ExcluirStorageDataModel
    {
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<LongInput> Storage { get; set; }

        /// <summary>
        /// Referência de storage
        /// </summary>
        [Display(Name = "Referência")]
        public IList<string> Referencia { get; set; }
    }
}
