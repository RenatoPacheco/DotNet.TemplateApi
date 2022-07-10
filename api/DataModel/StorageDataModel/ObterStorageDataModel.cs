using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.StorageDataModel
{
    public class ObterStorageDataModel
    {
        /// <summary>
        /// Alias de storage
        /// </summary>
        [FromRoute]
        public string Alias { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status { get; set; }
    }
}
