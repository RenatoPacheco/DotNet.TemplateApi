using System;

namespace TemplateApi.Infra.Interfaces
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
