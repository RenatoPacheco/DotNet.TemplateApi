using System;
using System.Data.SqlClient;

namespace DotNetCore.API.Template.Repositorio.Contexto
{
    public class Conexao : IDisposable
    {
        private SqlConnection _sessao;
        public SqlConnection Sessao
        {
            get => Sessao1 ?? IniciarSessao();
            private set => Sessao1 = value;
        }

        public SqlTransaction Transicao { get; private set; }
        public SqlConnection Sessao1 { get => _sessao; set => _sessao = value; }

        public void Dispose()
        {
            FecharSessao();
            GC.SuppressFinalize(this);

        }

        public bool HaSessao()
        {
            return !(Sessao1 is null);
        }

        public SqlConnection IniciarSessao()
        {
            if (!HaSessao())
            {
                Sessao1 = new SqlConnection(ConnectionStrings.Teste);
                Sessao1.Open();
            }
            return Sessao1;
        }

        public void FecharSessao()
        {
            if (HaSessao())
            {
                FecharTransicao();
                Sessao1.Close();
                Sessao1.Dispose();
                Sessao1 = null;
            }
        }

        public bool HaTransicao()
        {
            return !(Transicao is null);
        }

        public void IniciarTransicao()
        {
            if (!HaTransicao())
            {
                Transicao = Sessao1.BeginTransaction();
            }
        }

        public void FecharTransicao()
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
