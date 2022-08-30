using System.Collections.Generic;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.StorageDataModel
{
    public class FiltrarStorageDataModel
        : Common.FiltrarBaseDataModel<FiltrarStorageDataModel>
    {
        private EnumInput<ContextoCmd> _contexto;
        /// <summary>
        /// Informe o contexto da busca, sendo que o valor padrão é Embutir
        /// </summary>
        public EnumInput<ContextoCmd> Contexto
        {
            get => _contexto;
            set
            {
                _contexto = value;
                RegistarPropriedade(x => x.Contexto);
            }
        }

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
                RegistarPropriedade(x => x.Alias);
            }
        }

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de conteúdo
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
    }
}
