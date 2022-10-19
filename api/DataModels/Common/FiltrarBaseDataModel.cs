using System;
using BitHelp.Core.Extend;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.Common
{
    public abstract class FiltrarBaseDataModel<T>
    {
        private IList<string> _propriedadesRegistradas = new List<string>();

        private static string _texto;
        /// <summary>
        /// Texto com as palavras chaves para busca.
        /// </summary>
        public virtual string Texto
        {
            get => _texto;
            set
            {
                _texto = value;
                RegistarPropriedade(nameof(Texto));
            }
        }

        private IntInput _pagina;
        /// <summary>
        /// Página atual, com valor padrão 1, sendo qualquer valor menor que 1, será considerado o valor padrão.
        /// </summary>
        [Display(Name = "Página")]
        public virtual IntInput Pagina
        {
            get => _pagina;
            set
            {
                _pagina = value;
                RegistarPropriedade(nameof(Pagina));
            }
        }

        private IntInput _maximo;
        /// <summary>
        /// Máximo de registros por página, com valor padrão 100.
        /// Pode indicar um valor menor que um para buscar todos os registros.
        /// Se indicar qualquer valor menor que 1, será passado para 0, e buscará por tosos os registros.
        /// </summary>
        [Display(Name = "Máximo")]
        public virtual IntInput Maximo
        {
            get => _maximo;
            set
            {
                _maximo = value;
                RegistarPropriedade(nameof(Maximo));
            }
        }

        private BoolInput _calucularPaginacao;
        /// <summary>
        /// Informar se será feito o cáluculo da paginação.
        /// Por padrão o valor é false.
        /// Quando true retorna o total de resultados e o total de páginas.
        /// </summary>
        [Display(Name = "Calcular paginação")]
        public virtual BoolInput CalcularPaginacao
        {
            get => _calucularPaginacao;
            set
            {
                _calucularPaginacao = value;
                RegistarPropriedade(nameof(CalcularPaginacao));
            }
        }

        protected void RegistarPropriedade<P>(
            Expression<Func<T, P>> expressao)
        {
            RegistarPropriedade(expressao.PropertyPath());
        }

        protected void RegistarPropriedade(string propriedade)
        {
            if (!_propriedadesRegistradas.Contains(propriedade))
            {
                _propriedadesRegistradas.Add(propriedade);
            }
        }

        public bool PropriedadeRegistrada<P>(
            Expression<Func<T, P>> expressao)
        {
            string prop = expressao.PropertyPath();
            return _propriedadesRegistradas.Contains(prop);
        }
    }
}
