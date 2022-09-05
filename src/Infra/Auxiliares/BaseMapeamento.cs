using System;
using System.Linq;
using BitHelp.Core.Extend;
using System.Linq.Expressions;
using System.Collections.Generic;
using TemplateApi.Compartilhado.Extensoes;

namespace TemplateApi.Infra.Auxiliares
{
    internal abstract class BaseMapeamento<T>
        where T : class
    {
        private string _tabela;
        public string Tabela
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(RefSql))
                    return $"{_tabela} AS {RefSql}";
                else
                    return _tabela;
            }
        }

        protected string[] _ignorar = Array.Empty<string>();

        private string _refJson;
        public string RefJson
        {
            get { return _refJson; }
            set { _refJson = string.IsNullOrWhiteSpace(value) ? string.Empty : value; }
        }

        private string _refSql;
        public string RefSql
        {
            get { return _refSql; }
            set { _refSql = string.IsNullOrWhiteSpace(value) ? string.Empty : value; }
        }

        private readonly IDictionary<string, string> _colunas = new Dictionary<string, string>();

        private readonly IDictionary<string, string> _propriedades = new Dictionary<string, string>();

        public BaseMapeamento<T> Ignorar<P>(Expression<Func<T, P>> expression)
        {
            _ignorar = _ignorar.Concat(new string[] { expression.PropertyPath() }).ToArray();
            return this;
        }

        public string Coluna<P>(Expression<Func<T, P>> expression)
        {
            string referencia = expression.PropertyPath();

            if (!string.IsNullOrWhiteSpace(RefSql))
                return $"{RefSql}.{_colunas[referencia]}";
            else
                return _colunas[referencia];
        }

        public string Col<P>(Expression<Func<T, P>> expression)
        {
            return Coluna(expression);
        }

        public string Propriedade<P>(Expression<Func<T, P>> expression)
        {
            string referencia = expression.PropertyPath();
            string refJson = string.IsNullOrWhiteSpace(RefJson) ? string.Empty : $"{RefJson}.";

            return $"[{refJson}{_propriedades[referencia]}]";
        }

        public string Prop<P>(Expression<Func<T, P>> expression)
        {
            return Propriedade(expression);
        }

        protected void DefinnirTabela(string tabela)
        {
            _tabela = tabela.Trim();
        }

        protected void Associar<P>(Expression<Func<T, P>> expression, string sql)
        {
            string referencia = expression.PropertyPath();
            string json = referencia.StartToLower();
            _colunas.Add(referencia, sql);
            _propriedades.Add(referencia, json);
        }

        protected bool NaoIgnorar<P>(Expression<Func<T, P>> expression)
        {
            return !_ignorar.Contains(expression.PropertyPath());
        }

        protected string SqlParaJson<P>(Expression<Func<T, P>> expression)
        {
            return $"{Col(expression)} AS {Prop(expression)}";
        }
    }
}
