using System;

namespace DotNetCore.API.Template.Repositorio.Interfaces
{
    public interface IConexao 
        : IDisposable
    {
        bool HaSessao();

        bool HaTransicao();

        void IniciarTransicao();

        void SalvarTransicao();

        void DesfazerTransicao();
    }
}
