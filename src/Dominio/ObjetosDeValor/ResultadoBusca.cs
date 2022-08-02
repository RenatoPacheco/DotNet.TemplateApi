using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.ObjetosDeValor
{
    public class ResultadoBusca<T>
    {
        /// <summary>
        /// Indica o total de resultados encontrados, somando todas as páginas.
        /// Não será retornar de não for indicado para calcular a paginação.
        /// </summary>
        [Display(Name = "Total de resultados")]
        public long? TotalDeResultados { get; set; }

        /// <summary>
        /// Indica o ttal de páginas encontradas.
        /// Não será retornar de não for indicado para calcular a paginação.
        /// </summary>
        [Display(Name = "Total de páginas")]
        public long? TotalDePaginas { get; set; }

        private T[] _resultadosDaPaginaAtual = Array.Empty<T>();
        /// <summary>
        /// Retorna os resultados encontrados na página atual.
        /// </summary>
        [Display(Name = "Resultados da página atual")]
        public T[] ResultadosDaPaginaAtual
        {
            get => _resultadosDaPaginaAtual;
            set => _resultadosDaPaginaAtual = value ?? Array.Empty<T>();
        }

        public void CalcularPaginas(long total, long maximo)
        {
            TotalDeResultados = total;
            TotalDePaginas = maximo < 1 ? 1
                : total % maximo > 0 ? total / maximo + 1 : total / maximo;
        }

        public List<T> ToList()
        {
            return ResultadosDaPaginaAtual.ToList();
        }

        public T[] ToArray()
        {
            return ResultadosDaPaginaAtual.ToArray();
        }

        public T First()
        {
            return ResultadosDaPaginaAtual.First();
        }

        public T FirstOrDefault()
        {
            return ResultadosDaPaginaAtual.FirstOrDefault();
        }

        public T Last()
        {
            return ResultadosDaPaginaAtual.Last();
        }

        public T LastOrDefault()
        {
            return ResultadosDaPaginaAtual.LastOrDefault();
        }
    }
}
