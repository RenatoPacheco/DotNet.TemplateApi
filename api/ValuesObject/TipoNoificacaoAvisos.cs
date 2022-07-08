using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.Template.Site.ValuesObject
{
    public enum TipoNoificacaoAvisos
    {
        Erro,
        Sucesso,
        [Display(Name = "Atenção")]
        Atencao,
        [Display(Name = "Não autorizado")]
        NaoAutorizado,
        [Display(Name = "Não encontrado")]
        NaoEncontrado
    }
}