using System;
using System.Linq;
using BitHelp.Core.Extend;
using System.Linq.Expressions;
using System.Collections.Generic;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Repositorio.Adaptadores;

namespace TemplateApi.Repositorio.Auxiliares
{
    internal abstract class MapeamentoBase<T>
        where T : class
    {
        public abstract string Tabela { get; }

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

        public MapeamentoBase<T> Ignorar<P>(Expression<Func<T, P>> expression)
        {
            _ignorar = _ignorar.Concat(new string[] { expression.PropertyPath() }).ToArray();
            return this;
        }

        public string Col<P>(Expression<Func<T, P>> expression)
        {
            string referencia = expression.PropertyPath();
            return _colunas[referencia];
        }

        public string Coluna<P>(Expression<Func<T, P>> expression)
        {
            string referencia = expression.PropertyPath();
            return _colunas[referencia];
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
            string referencia = expression.PropertyPath();
            string refSql = string.IsNullOrWhiteSpace(RefSql) ? string.Empty : $"{RefSql}.";
            string refJson = string.IsNullOrWhiteSpace(RefJson) ? string.Empty : $"{RefJson}.";

            return $"{refSql}[{_colunas[referencia]}] AS [{refJson}{_propriedades[referencia]}]";
        }

        protected string CharParaStatus<P>(Expression<Func<T, P>> expression)
        {
            string referencia = expression.PropertyPath();
            string refSql = string.IsNullOrWhiteSpace(RefSql) ? string.Empty : $"{RefSql}.";
            string refJson = string.IsNullOrWhiteSpace(RefJson) ? string.Empty : $"{RefJson}.";

            return $"[{refJson}{_propriedades[referencia]}] = {StatusAdapt.SqlParaEnum($"{refSql}[{_colunas[referencia]}]")}";
        }

        protected string CharParaBoolean<P>(Expression<Func<T, P>> expression)
        {
            string referencia = expression.PropertyPath();
            string refSql = string.IsNullOrWhiteSpace(RefSql) ? string.Empty : $"{RefSql}.";
            string refJson = string.IsNullOrWhiteSpace(RefJson) ? string.Empty : $"{RefJson}.";

            return $"[{refJson}{_propriedades[referencia]}] = {CharAdapt.SqlParaBoolean($"{refSql}[{_colunas[referencia]}]")}";
        }
    }
}
