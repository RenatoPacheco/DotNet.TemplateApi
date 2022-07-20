using System;

namespace TemplateApi.Repositorio.Interfaces
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
