using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.ConteudoDataModel
{
    public class ExcluirConteudoDataModel
        : Common.BaseDataModel<ExcluirConteudoDataModel>
    {
        private IntInput _conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IntInput Conteudo
        {
            get => _conteudo;
            set
            {
                _conteudo = value;
                RegistarPropriedade(x => x.Conteudo);
            }
        }
    }
}
