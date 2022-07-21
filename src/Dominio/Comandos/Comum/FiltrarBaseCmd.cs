using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Comandos.Comum
{
    public abstract class FiltrarBaseCmd
    {
        /// <summary>
        /// Texto para busca
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Página atual, com valor padrão 1, sendo qulquer valor menor que 1, será considerado o valor padrão
        /// </summary>
        [Display(Name = "Página")]
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Máximo de registros por página, com valor padrão 100, sendo que, qualquer valor menor que 1, será feita a consulta, sem paginação.
        /// </summary>
        [Display(Name = "Máximo")]
        public int Maximo { get; set; } = 100;
    }
}
