using System;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.Repositorio.Contexto
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
