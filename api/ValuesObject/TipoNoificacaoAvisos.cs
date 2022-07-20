using System.ComponentModel.DataAnnotations;

namespace TemplateApi.Api.ValuesObject
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