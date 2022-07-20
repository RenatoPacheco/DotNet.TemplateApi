using System;
using System.Linq.Expressions;
using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;

namespace TemplateApi.Dominio.Escopos
{
    public class BaseEscp<TClasse>
        where TClasse : ISelfValidation
    {
        public BaseEscp(TClasse entidade)
        {
            _entidade = entidade;
        }

        protected readonly TClasse _entidade;

        public void EhRequerido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.Notifications.RequiredIsValid(_entidade, expressao);
        }

        public void EhRequeridoSeOutroForNulo(Expression<Func<TClasse, object>> expressao, Expression<Func<TClasse, object>> expressaoCompare)
        {
            _entidade.Notifications.RequiredIfOtherNotNullIsValid(_entidade, expressao, expressaoCompare);
        }

        public void LimparReferencia(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.Notifications.RemoveAtReference(expressao);
        }
    }
}