using System;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Interfaces;

namespace DotNetCore.API.Template.Repositorio.Contexto
{
    public sealed class UnidadeTrabalho
        : IUnidadeTrabalho
    {
        public UnidadeTrabalho(IConexao connexao)
        {
            _conexao = connexao;
        }

        private readonly IConexao _conexao;
        private bool _possoEncerrar = true;

        public void Dispose()
        {
            if (_conexao.HaSessao())
            {
                if (_conexao.HaTransicao())
                {
                    _conexao.SalvarTransicao();
                }
            }

            GC.SuppressFinalize(this);
        }

        public ITransicao Requisitar()
        {
            ITransicao reultado = new Transicao(_conexao, _possoEncerrar);
            _possoEncerrar = false;
            return reultado;
        }

        public bool PossoIniciar()
        {
            return !_possoEncerrar;
        }
    }
}
