using System;
using BitHelp.Core.Extend;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TemplateApi.Api.DataModels.Common
{
    public abstract class BaseDataModel
    {
        private IList<string> _propriedadesRegistradas = new List<string>();

        protected void RegistrarPropriedade(
            [CallerMemberName] string nome = null)
        {
            if (!_propriedadesRegistradas.Contains(nome))
            {
                _propriedadesRegistradas.Add(nome);
            }
        }

        public bool PropriedadeRegistrada(string nome)
        {
            return _propriedadesRegistradas.Contains(nome);
        }
    }

    public abstract class BaseDataModel<T>
        : BaseDataModel
    {
        protected void RegistrarPropriedade<P>(
            Expression<Func<T, P>> expressao)
        {
            string prop = expressao.PropertyPath();
            RegistrarPropriedade(prop);
        }

        public bool PropriedadeRegistrada<P>(
            Expression<Func<T, P>> expressao)
        {
            string prop = expressao.PropertyPath();
            return PropriedadeRegistrada(prop);
        }
    }
}