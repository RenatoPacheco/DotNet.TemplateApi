
namespace TemplateApi.Repositorio.Interfaces
{
    public interface IUnidadeTrabalho
    {
        ITransicao Requisitar();

        bool PossoIniciar();
    }
}
