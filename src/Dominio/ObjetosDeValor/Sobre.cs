using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
{
    /// <summary>
    /// Obtém informações básicas sobre a aplicação
    /// </summary>
    public class Sobre
    {
        /// <summary>
        /// Nome da aplicação
        /// </summary>
        public string Nome => AppSettings.Nome;

        /// <summary>
        /// Versão atual da aplicação
        /// </summary>
        [Display(Name = "Versão")]
        public string Versao => AppSettings.Versao;

        /// <summary>
        /// Servidor atual que a aplicação está sendo executada
        /// </summary>
        public string Servidor => DetectarServidor.Servidor;
    }
}
