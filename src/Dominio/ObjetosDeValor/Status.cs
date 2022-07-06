using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
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
