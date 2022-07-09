using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Comandos.Comum;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Site.DataModel.UsuarioDataModel
{
    public class FiltrarUsuarioDataModel : FiltrarBaseCmd
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

        private IList<EnumInput<Status>> _status;
        /// <summary>
        /// Status de usuário
        /// </summary>
        public IList<EnumInput<Status>> Status
        {
            get => _status ??= new List<EnumInput<Status>>();
            set => _status = value ?? new List<EnumInput<Status>>();
        }
    }
}
