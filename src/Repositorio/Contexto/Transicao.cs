using System;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Interfaces;

namespace DotNetCore.API.Template.Repositorio.Contexto
{
    public class Transicao : ITransicao
    {
        public Transicao(IConexao connexao)
        {
            _conexao = connexao;
            _possoEncerrar = false;
        }

        internal Transicao(
            IConexao connexao,
            bool possoEncerrar)
        {
            _conexao = connexao;
            _possoEncerrar = possoEncerrar;
        }

        private IConexao _conexao;
        private readonly bool _possoEncerrar;

        public void Dispose()
        {
            if (_conexao != null && _possoEncerrar)
            {
                if (_conexao.HaSessao())
                {
                    if (_conexao.HaTransicao())
                    {
                        _conexao.SalvarTransicao();
                    }
                }
            }

            GC.SuppressFinalize(this);
        }

        public void Iniciar()
        {
            if (_conexao?.HaSessao() ?? false)
            {
                if (!_conexao.HaTransicao())
                {
                    _conexao.IniciarTransicao();
                }
            }
        }

        public void Salvar()
        {
            if (_possoEncerrar)
            {
                if (_conexao?.HaSessao() ?? false)
                {
                    if (_conexao.HaTransicao())
                    {
                        _conexao.SalvarTransicao();
                    }
                }

                _conexao = null;
            }
        }

        public void Desfazer()
        {
            if (_possoEncerrar)
            {
                if (_conexao?.HaSessao() ?? false)
                {
                    if (_conexao.HaTransicao())
                    {
                        _conexao.DesfazerTransicao();
                    }
                }

                _conexao = null;
            }
        }
    }
}
