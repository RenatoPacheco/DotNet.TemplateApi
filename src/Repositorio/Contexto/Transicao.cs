using System;
using DotNetCore.API.Template.Dominio.Interfaces;


namespace DotNetCore.API.Template.Repositorio.Contexto
{
    public class Transicao : ITransicao
    {
        public Transicao(Conexao connexao)
        {
            _connexao = connexao;
            _possoEncerrar = false;
        }

        internal Transicao(
            Conexao connexao,
            bool possoEncerrar)
        {
            _connexao = connexao;
            _possoEncerrar = possoEncerrar;
        }

        private Conexao _connexao;
        private readonly bool _possoEncerrar;

        public void Dispose()
        {
            if (!object.Equals(_connexao, null) && _possoEncerrar)
            {
                if (_connexao.HaSessao())
                {
                    if (_connexao.HaTransicao())
                    {
                        _connexao.FecharTransicao();
                    }
                }
            }

            GC.SuppressFinalize(this);
        }

        internal void Inicializar()
        {
            if (_connexao.HaSessao())
            {
                if (!_connexao.HaTransicao())
                {
                    _connexao.IniciarTransicao();
                }
            }
        }

        public void Salvar()
        {
            if (_possoEncerrar)
            {
                if (_connexao.HaSessao())
                {
                    if (_connexao.HaTransicao())
                    {
                        _connexao.FecharTransicao();
                    }
                }

                _connexao = null;
            }
        }

        public void Desfazer()
        {
            if (_possoEncerrar)
            {
                if (_connexao.HaSessao())
                {
                    if (_connexao.HaTransicao())
                    {
                        _connexao.DesfazerTransicao();
                    }
                }

                _connexao = null;
            }
        }
    }
}
