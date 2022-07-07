using System;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Comandos.Comum;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.UsuarioDataModel
{
    public class FiltrarUsuarioDataModel : FiltrarBaseCmd
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

        private Status[] _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public Status[] Status
        {
            get => _status ??= Array.Empty<Status>();
            set => _status = value ?? Array.Empty<Status>();
        }
    }
}
