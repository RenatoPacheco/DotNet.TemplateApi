namespace DotNetCore.API.Template.Dominio.Interfaces
{
    public interface IUnidadeTrabalho
    {
        ITransicao Requisitar();

        bool PossoIniciar();
    }
}
