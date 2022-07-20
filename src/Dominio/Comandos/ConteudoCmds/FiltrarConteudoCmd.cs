using BitHelp.Core.Validation;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.ConteudoCmds
{
    public class FiltrarConteudoCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarConteudoCmd()
        {
            _escopo = new ConteudoEscp<FiltrarConteudoCmd>(this);
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

        private IList<Status> _status;
        /// <summary>
        /// Status de conteúdo
        /// </summary>
        public IList<Status> Status
        {
            get => _status ??= new List<Status>();
            set
            {
                _status = value ?? new List<Status>();
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        #region Auto validação

        protected ConteudoEscp<FiltrarConteudoCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            return Notifications.IsValid();
        }

        #endregion
    }
}
