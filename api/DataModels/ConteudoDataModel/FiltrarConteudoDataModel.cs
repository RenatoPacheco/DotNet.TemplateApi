using System.Collections.Generic;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.ConteudoDataModel
{
    public class FiltrarConteudoDataModel 
        : Common.FiltrarBaseDataModel<FiltrarConteudoDataModel>
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
                RegistarPropriedade();
            }
        }

        private IList<IntInput> _conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<IntInput> Conteudo
        {
            get => _conteudo;
            set
            {
                _conteudo = value;
                RegistarPropriedade();
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
                RegistarPropriedade();
            }
        }
    }
}
