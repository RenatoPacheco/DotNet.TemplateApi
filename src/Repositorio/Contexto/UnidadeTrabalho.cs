using System;
using DotNetCore.API.Template.Dominio.Interfaces;

namespace DotNetCore.API.Template.Repositorio.Contexto
{
    public sealed class UnidadeTrabalho
        : IUnidadeTrabalho
    {
        public UnidadeTrabalho(Conexao connexao)
        {
            _connexao = connexao;
        }

        private readonly Conexao _connexao;
        private bool _possoEncerrar = true;

        public void Dispose()
        {
            if (_connexao.HaSessao())
            {
                if (_connexao.HaTransicao())
                {
                    _connexao.FecharTransicao();
                }
            }

            GC.SuppressFinalize(this);
        }

        public ITransicao Requisitar()
        {
            Transicao reultado = new Transicao(_connexao, _possoEncerrar);
            _possoEncerrar = false;
            return reultado;
        }

        public bool DevoInicializar()
        {
            return !_possoEncerrar;
        }
    }
}
