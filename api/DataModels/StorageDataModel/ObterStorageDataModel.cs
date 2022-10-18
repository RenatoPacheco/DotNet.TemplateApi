using System.Collections.Generic;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel
{
    public class ObterStorageDataModel
        : Common.BaseDataModel<ObterStorageDataModel>
    {
        private string _alias;
        /// <summary>
        /// Alias de storage
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                _alias = value;
                RegistarPropriedade(x => x.Alias);
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de srorage
        /// </summary>
        public IList<EnumInput<Status>> Status
        {
            get => _status;
            set
            {
                _status = value;
                RegistarPropriedade(x => x.Status);
            }
        }

        private BoolInput _download;
        /// <summary>
        /// Informe true para fazer download do arquivo 
        /// </summary>
        public BoolInput Download
        {
            get => _download;
            set
            {
                _download = value;
                RegistarPropriedade(x => x.Download);
            }
        }
    }
}
