using System;
using System.Data.SqlClient;
using DotNetCore.API.Template.Repositorio.Interfaces;
namespace DotNetCore.API.Template.Repositorio.Contexto
{
    public class Conexao 
        : IConexao, IDisposable
    {
        private SqlConnection _sessao;
        public SqlConnection Sessao
        {
            get => _sessao ?? IniciarSessao();
            private set => _sessao = value;
        }

        public SqlTransaction Transicao { get; private set; }

        public void Dispose()
        {
            FecharSessao();
            GC.SuppressFinalize(this);

        }

        public bool HaSessao()
        {
            return _sessao != null;
        }

        public SqlConnection IniciarSessao()
        {
            if (!HaSessao())
            {
                _sessao = new SqlConnection(ConnectionStrings.Testando);
                _sessao.Open();
            }
            return _sessao;
        }

        public void FecharSessao()
        {
            if (HaSessao())
            {
                SalvarTransicao();
                _sessao.Close();
                _sessao.Dispose();
                _sessao = null;
            }
        }

        public bool HaTransicao()
        {
            return Transicao != null;
        }

        public void IniciarTransicao()
        {
            if (!HaTransicao())
            {
                Transicao = Sessao.BeginTransaction();
            }
        }

        public void SalvarTransicao()
        {
            if (HaTransicao())
            {
                Transicao.Commit();
                Transicao = null;
            }
        }

        public void DesfazerTransicao()
        {
            if (HaTransicao())
            {
                Transicao.Rollback();
                Transicao = null;
            }
        }
    }
}
