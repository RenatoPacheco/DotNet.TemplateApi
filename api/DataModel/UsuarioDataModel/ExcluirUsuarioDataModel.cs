using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.UsuarioDataModel
{
    public class ExcluirUsuarioDataModel
    {
        private IList<IntInput> _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public IList<IntInput> Usuario
        {
            get => _usuario ??= new List<IntInput>();
            set => _usuario = value ?? new List<IntInput>();
        }
    }
}
