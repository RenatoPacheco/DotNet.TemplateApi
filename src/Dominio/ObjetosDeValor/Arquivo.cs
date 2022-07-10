using DotNetCore.API.Template.Dominio.Interfaces;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
{
    public abstract class Arquivo : IArquivo
    {
        public string Nome { get; set; }

        public string Alias { get; set; }

        public string Diretorio { get; set; }

        public string Extensao { get; set; }

        public string Tipo { get; set; }

        public long Peso { get; set; }
        
        public string Referencia { get; set; }

        public abstract void Salvar();

        public abstract void Excluir();
    }
}
