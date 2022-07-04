using System;

namespace DotNetCore.API.Template.Dominio.Interfaces
{
    public interface ITransicao : IDisposable
    {
        void Salvar();

        void Desfazer();
    }
}