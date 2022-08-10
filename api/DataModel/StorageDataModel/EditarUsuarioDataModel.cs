using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.StorageDataModel
{
    public class EditarStorageDataModel
    {
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public LongInput Storage { get; set; }

        /// <summary>
        /// Nome de storage
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public EnumInput<Status> Status { get; set; }
    }
}
