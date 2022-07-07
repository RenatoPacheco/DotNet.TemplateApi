using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Site.DataModel.UsuarioDataModel
{
    public class ExcluirUsuarioDataModel
    {
        private int[] _usuario;
        /// <summary>
        /// Identificador de usuário
        /// </summary>
        [Display(Name = "Usuário")]
        public int[] Usuario
        {
            get => _usuario ??= Array.Empty<int>();
            set => _usuario = value ?? Array.Empty<int>();
        }
    }
}
