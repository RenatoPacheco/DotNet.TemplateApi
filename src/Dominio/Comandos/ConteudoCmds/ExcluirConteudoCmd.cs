using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Escopos;

namespace DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds
{
    public class ExcluirConteudoCmd : ISelfValidation
    {
        public ExcluirConteudoCmd()
        {
            _escopo = new ConteudoEscp<ExcluirConteudoCmd>(this);
        }

        private IList<int> _Conteudo;
        /// <summary>
        /// Identificador de conteúdo
        /// </summary>
        [Display(Name = "Conteúdo")]
        public IList<int> Conteudo
        {
            get => _Conteudo ??= new List<int>();
            set
            {
                _Conteudo = value ?? new List<int>();
                _escopo.IdEhValido(x => x.Conteudo);
            }
        }

        #region Auto validação

        protected ConteudoEscp<ExcluirConteudoCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequerido(x => x.Conteudo);

            return Notifications.IsValid();
        }

        #endregion
    }
}
