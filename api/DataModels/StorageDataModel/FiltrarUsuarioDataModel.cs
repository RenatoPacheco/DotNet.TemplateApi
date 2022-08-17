using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel
{
    public class FiltrarStorageDataModel : FiltrarBaseCmd
    {
        /// <summary>
        /// Informe o contexto da busca, sendo que o valor padrão é Embutir
        /// </summary>
        public EnumInput<ContextoCmd> Contexto { get; set; }

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
