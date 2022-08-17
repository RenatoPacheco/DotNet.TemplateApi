using BitHelp.Core.Validation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Escopos;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Comandos.ConteudoCmds
{
    public class InserirConteudoCmd : ISelfValidation
    {
        public InserirConteudoCmd()
        {
            _escopo = new ConteudoEscp<InserirConteudoCmd>(this);
        }

        private string _titulo;
        /// <summary>
        /// Título de conteúdo
        /// </summary>
        [Display(Name = "Título")]
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                _escopo.TituloEhValido(x => x.Titulo);
            }
        }

        private string _alias;
        /// <summary>
        /// Alias de conteúdo
        /// </summary>
        public string Alias
        {
            get => _alias;
            set
            {
                _alias = value;
                _escopo.AliasEhValido(x => x.Alias);
            }
        }

        private string _texto;
        /// <summary>
        /// Texto de conteúdo
        /// </summary>
        public string Texto
        {
            get => _texto;
            set
            {
                _texto = value;
                _escopo.TextoEhValido(x => x.Texto);
            }
        }

        private Status? _status;
        /// <summary>
        /// Status de conteúdo
        /// </summary>
        public Status? Status
        {
            get => _status;
            set
            {
                _status = value;
                _escopo.StatusEhValido(x => x.Status);
            }
        }

        public void Aplicar(ref Conteudo dados)
        {
            dados = new Conteudo(
                Titulo, Alias, Texto, Status);
        }

        public void Desfazer(ref Conteudo dados) => dados = null;

        #region Auto validação

        protected ConteudoEscp<InserirConteudoCmd> _escopo;

        [JsonIgnore]
        public ValidationNotification Notifications { get; set; } = new ValidationNotification();

        public virtual bool IsValid()
        {
            _escopo.EhRequerido(x => x.Titulo);
            _escopo.EhRequerido(x => x.Alias);
            _escopo.EhRequerido(x => x.Texto);
            _escopo.EhRequerido(x => x.Status);

            return Notifications.IsValid();
        }

        #endregion
    }
}
