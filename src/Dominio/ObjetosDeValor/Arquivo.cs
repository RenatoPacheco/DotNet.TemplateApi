using System.ComponentModel.DataAnnotations;
using TemplateApi.Dominio.Interfaces;

namespace TemplateApi.Dominio.ObjetosDeValor
{
    public abstract class Arquivo : IArquivo
    {
        public string Nome { get; set; }

        public string Alias { get; set; }

        [Display(Name = "Diretório")]
        public string Diretorio { get; set; }

        [Display(Name = "Extensão")]
        public string Extensao { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

        public string Tipo { get; set; }

        public string Checksum { get; set; }

        public long Peso { get; set; }
        
        public string Referencia { get; set; }

        public abstract void Salvar();

        public abstract void Excluir();
    }
}
