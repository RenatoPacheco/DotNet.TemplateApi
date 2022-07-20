namespace TemplateApi.Dominio.Interfaces
{
    public interface IUnidadeTrabalho
    {
        ITransicao Requisitar();

        bool PossoIniciar();
    }
}
