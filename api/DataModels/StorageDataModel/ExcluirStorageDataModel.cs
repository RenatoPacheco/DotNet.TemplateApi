using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel
{
    public class ExcluirStorageDataModel
        : Common.BaseDataModel<ExcluirStorageDataModel>
    {
        private IList<LongInput> _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public IList<LongInput> Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                RegistarPropriedade(x => x.Storage);
            }
        }

        private IList<string> _referencia;
        /// <summary>
        /// Referência de storage
        /// </summary>
        [Display(Name = "Referência")]
        public IList<string> Referencia
        {
            get => _referencia;
            set
            {
                _referencia = value;
                RegistarPropriedade(x => x.Referencia);
            }
        }
    }
}
