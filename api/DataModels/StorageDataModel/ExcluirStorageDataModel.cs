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
                RegistrarPropriedade();
            }
        }

        private IList<string> _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        public IList<string> Alias
        {
            get => _alias;
            set
            {
                _alias = value;
                RegistrarPropriedade();
            }
        }
    }
}
