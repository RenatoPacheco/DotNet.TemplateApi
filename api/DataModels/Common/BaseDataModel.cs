using System;
using BitHelp.Core.Extend;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace TemplateApi.Api.DataModels.Common
{
    public abstract class BaseDataModel<T>
    {
        private IList<string> _propriedadesRegistradas = new List<string>();

        protected void RegistarPropriedade<P>(
            Expression<Func<T, P>> expressao)
        {
            string prop = expressao.PropertyPath();
            if (!_propriedadesRegistradas.Contains(prop))
            {
                _propriedadesRegistradas.Add(prop);
            }
        }

        public bool PropriedadeRegistrada<P>(
            Expression<Func<T, P>> expressao)
        {
            string prop = expressao.PropertyPath();
            return _propriedadesRegistradas.Contains(prop);
        }
    }
}
