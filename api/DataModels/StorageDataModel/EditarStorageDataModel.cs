using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel
{
    public class EditarStorageDataModel
        : Common.BaseDataModel<EditarStorageDataModel>
    {
        private LongInput _storage;
        /// <summary>
        /// Identificador de storage
        /// </summary>
        public LongInput Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                RegistarPropriedade();
            }
        }

        private string _nome;
        /// <summary>
        /// Nome de storage
        /// </summary>
        public string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                RegistarPropriedade();
            }
        }

        private EnumInput<Status> _status;
        /// <summary>
        /// Status de srorage
        /// </summary>
        public EnumInput<Status> Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistarPropriedade();
            }
        }
    }
}
