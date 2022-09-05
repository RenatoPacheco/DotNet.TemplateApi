using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using TemplateApi.Infra.Interfaces;

namespace TemplateApi.Infra.Contexto
{
    public abstract class ConexaoMsSql
        : IConexao, IDisposable
    {
        protected abstract string ConnectionString { get; }

        private static bool Configurado { get; set; }

        private static void Configurar()
        {
            if (!Configurado)
            {
                SqlMapper.PurgeQueryCache();
                SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
                Configurado = true;
            }
        }

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
                Configurar();
                _sessao = new SqlConnection(ConnectionString);
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
