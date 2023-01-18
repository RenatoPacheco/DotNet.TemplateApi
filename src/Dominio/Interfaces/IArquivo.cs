using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.Interfaces
{
    public interface IArquivo
    {
        string Nome { get; set; }

        string Alias { get; set; }

        [Display(Name = "Diretório")]
        string Diretorio { get; set; }

        [Display(Name = "Extensão")]
        string Extensao { get; set; }

        [Display(Name = "URL")]
        string Url { get; set; }

        string Tipo { get; set; }

        long Peso { get; set; }

        string Checksum { get; set; }

        string Referencia { get; set; }
    }
}
