using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.StorageDataModel
{
    public class EditarStorageDataModel
    {
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public LongInput? Storage { get; set; }

        /// <summary>
        /// Nome de storage
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Status de usuário
        /// </summary>
        public EnumInput<Status>? Status { get; set; }
    }
}
