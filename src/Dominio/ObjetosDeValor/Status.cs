using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Dominio.ObjetosDeValor
{
    /// <summary>
    /// Opções de status
    /// </summary>
    public enum Status
    {
        Inativo,
        Ativo,
        [Display(Name = "Excluído")]
        Excluido
    }
}
