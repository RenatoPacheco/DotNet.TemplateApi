using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel
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
