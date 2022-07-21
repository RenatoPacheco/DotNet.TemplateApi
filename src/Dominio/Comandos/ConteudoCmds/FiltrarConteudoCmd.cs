using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Dominio.Escopos;
using System.Text.Json.Serialization;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Comandos.ConteudoCmds
{
    public class FiltrarConteudoCmd
        : Comum.FiltrarBaseCmd, ISelfValidation
    {
        public FiltrarConteudoCmd()
        {
            _escopo = new ConteudoEscp<FiltrarConteudoCmd>(this);
        }

        private ContextoCmd _contexto;
        /// <summary>
        /// Informe o contexto da busca, sendo que o valor padrão é Embutir
        /// </summary>
        public ContextoCmd Contexto
        {
            get => _contexto;
            set
            {
                _contexto = value;
                this.RemoveAtReference(x => x.Contexto);
            }
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
