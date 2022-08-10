using System.ComponentModel.DataAnnotations;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.ConteudoDataModel
{
    public class InserirConteudoDataModel
    {
        /// <summary>
        /// Título de conteúdo
        /// </summary>
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        /// <summary>
        /// Alias de conteúdo
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Texto de conteúdo
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Status de conteúdo
        /// </summary>
        public EnumInput<Status> Status { get; set; }
    }
}
