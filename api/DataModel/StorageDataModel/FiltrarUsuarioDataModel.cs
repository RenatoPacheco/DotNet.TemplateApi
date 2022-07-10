using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Comandos.Comum;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.StorageDataModel
{
    public class FiltrarStorageDataModel : FiltrarBaseCmd
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

        /// <summary>
        /// Alias de storage
        /// </summary>
        public IList<string> Alias { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status { get; set; }
    }
}
