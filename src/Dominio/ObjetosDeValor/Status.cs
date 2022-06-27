using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
{
    public enum Status
    {
        Inativo,
        Ativo,
        [Display(Name = "Excluído")]
        Excluido
    }
}
