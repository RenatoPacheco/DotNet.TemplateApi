using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Comandos.Comum
{
    public abstract class FiltrarBaseCmd
    {
        private static string _texto;
        /// <summary>
        /// Texto com as palavras chaves para busca.
        /// </summary>
        public string Texto 
        { 
            get => _texto; 
            set => _texto = value;
        }

        private int _pagina = 1;
        /// <summary>
        /// Página atual, com valor padrão 1, sendo qualquer valor menor que 1, será considerado o valor padrão.
        /// </summary>
        [Display(Name = "Página")]
        public int Pagina
        { 
            get => _pagina;
            set => _pagina = value < 1 ? 1 : value; 
        }

        private int _maximo = 100;
        /// <summary>
        /// Máximo de registros por página, com valor padrão 100.
        /// Pode indicar um valor menor que um para buscar todos os registros.
        /// Se indicar qualquer valor menor que 1, será passado para 0, e buscará por tosos os registros.
        /// </summary>
        [Display(Name = "Máximo")]
        public int Maximo
        {
            get => _maximo;
            set => _maximo = value < 1 ? 0 : value;
        }

        private bool _calucularPaginacao;
        /// <summary>
        /// Informar se será feito o cáluculo da paginação.
        /// Por padrão o valor é false.
        /// Quando true retorna o total de resultados e o total de páginas.
        /// </summary>
        [Display(Name = "Calcular paginação")]
        public bool CalcularPaginacao
        {
            get => _calucularPaginacao;
            set => _calucularPaginacao = value;
        }

    }
}
