using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.ConteudoDataModel
{
    public class ExcluirConteudoDataModel
        : Common.BaseDataModel<ExcluirConteudoDataModel>
    {
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
    }
}
