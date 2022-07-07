using System;
using System.Linq.Expressions;
using BitHelp.Core.Validation;
using BitHelp.Core.Validation.Extends;

namespace DotNetCore.API.Template.Dominio.Escopos
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

        public void LimparReferencia(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.Notifications.RemoveAtReference(expressao);
        }
    }
}