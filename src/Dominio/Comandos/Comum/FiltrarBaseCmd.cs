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
        /// Página atual
        /// </summary>
        [Display(Name = "Página")]
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Máximo de registros por página
        /// </summary>
        [Display(Name = "Máximo")]
        public int Maximo { get; set; } = 100;

        /// <summary>
        /// Calcular a paginação
        /// </summary>
        [Display(Name = "Paginação")]
        public bool Paginacao { get; set; } = true;
    }
}
