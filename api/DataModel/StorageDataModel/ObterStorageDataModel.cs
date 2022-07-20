using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.StorageDataModel
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

        /// <summary>
        /// Informe true para fazer download do arquivo 
        /// </summary>
        public BoolInput? Download { get; set; }
    }
}
