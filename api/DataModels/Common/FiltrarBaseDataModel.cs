using BitHelp.Core.Validation.Extends;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhados.ObjetosDeValor;
using TemplateApi.Compartilhados.Validacoes.Extensoes;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Api.DataModels.Common {

    public abstract class FiltrarBaseDataModel<T>
        : BaseDataModel<T> {

        private static string _texto;
        /// <summary>
        /// Texto com as palavras chaves para busca.
        /// </summary>
        public virtual string Texto {
            get => _texto;
            set {
                _texto = value;
                RegistrarPropriedade();
            }
        }

        private IntInput _pagina;
        /// <summary>
        /// Página atual, com valor padrão 1, sendo qualquer valor menor que 1, será considerado o valor padrão.
        /// </summary>
        [Display(Name = "Página")]
        public virtual IntInput Pagina {
            get => _pagina;
            set {
                _pagina = value;
                this.RemoveAtReference(x => x.Pagina);
                this.InputTypeIsValid(x => x.Pagina);
                RegistrarPropriedade();
            }
        }

        private IntInput _maximo;
        /// <summary>
        /// Máximo de registros por página, com valor padrão 100.
        /// Pode indicar um valor menor que um para buscar todos os registros.
        /// Se indicar qualquer valor menor que 1, será passado para 0, e buscará por todos os registros.
        /// </summary>
        [Display(Name = "Máximo")]
        public virtual IntInput Maximo {
            get => _maximo;
            set {
                _maximo = value;
                this.RemoveAtReference(x => x.Maximo);
                this.InputTypeIsValid(x => x.Maximo);
                RegistrarPropriedade();
            }
        }

        private BoolInput _calucularPaginacao;
        /// <summary>
        /// Informar se será feito o cáluculo da paginação.
        /// Por padrão o valor é false.
        /// Quando true retorna o total de resultados e o total de páginas.
        /// </summary>
        [Display(Name = "Calcular paginação")]
        public virtual BoolInput CalcularPaginacao {
            get => _calucularPaginacao;
            set {
                _calucularPaginacao = value;
                this.RemoveAtReference(x => x.CalcularPaginacao);
                this.InputTypeIsValid(x => x.CalcularPaginacao);
                RegistrarPropriedade();
            }
        }

        protected void AplicarBase(FiltrarBaseCmd resultado) {

            if (PropriedadeRegistrada(nameof(Texto))) {
                resultado.Texto = Texto;
            }

            if (PropriedadeRegistrada(nameof(Pagina))) {
                if (!this.HasNotification(x => x.Pagina)) {
                    resultado.Pagina = (int)Pagina;
                }
            }

            if (PropriedadeRegistrada(nameof(Maximo))) {
                if (!this.HasNotification(x => x.Maximo)) {
                    resultado.Maximo = (int)Maximo;
                }
            }

            if (PropriedadeRegistrada(nameof(CalcularPaginacao))) {
                if (!this.HasNotification(x => x.CalcularPaginacao)) {
                    resultado.CalcularPaginacao = (bool)CalcularPaginacao;
                }
            }

        }
    }
}
