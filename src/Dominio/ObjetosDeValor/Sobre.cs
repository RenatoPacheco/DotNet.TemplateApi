using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.ObjetosDeValor
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
        /// Informa o ambiente atual que pode ser producao ou desenvovlimento
        /// </summary>
        public string Ambiente => AppSettings.Ambiente;

        /// <summary>
        /// Informa se aplicação está em desenvolvimento ou não
        /// </summary>
        public bool EhDesenvolvimento => AppSettings.EhDesenvolvimento;
    }
}
