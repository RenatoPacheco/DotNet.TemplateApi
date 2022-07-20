using System;
using System.Linq.Expressions;
using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Escopos.Comum
{
    public class BaseEscopo<T>
        where T : ISelfValidation
    {
        public BaseEscopo(T entidade)
        {
            _entidade = entidade;
        }

        protected readonly T _entidade;

        public void EhRequerido(Expression<Func<T, object>> expressao)
        {
            _entidade.Notifications.RequiredIsValid(_entidade, expressao);
        }

        public void EhRequeridoSeOutroForNulo(Expression<Func<T, object>> expressao, Expression<Func<T, object>> expressaoCompare)
        {
            _entidade.Notifications.RequiredIfOtherNotNullIsValid(_entidade, expressao, expressaoCompare);
        }

        public void LimparReferencia(Expression<Func<T, object>> expressao)
        {
            _entidade.Notifications.RemoveAtReference(expressao);
        }
    }
}