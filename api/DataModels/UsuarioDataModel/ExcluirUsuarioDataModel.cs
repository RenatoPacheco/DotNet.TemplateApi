using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.UsuarioDataModel
{
    public class ExcluirUsuarioDataModel
        : Common.BaseDataModel<ExcluirUsuarioDataModel>
    {
        private IList<IntInput> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario
        {
            get => _usuario;
            set
            {
                _usuario = value;
                RegistarPropriedade();
            }
        }
    }
}
