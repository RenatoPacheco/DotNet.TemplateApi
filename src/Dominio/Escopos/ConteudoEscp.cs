using System;
using BitHelp.Core.Validation;
using System.Linq.Expressions;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.Validacoes.Extensoes;

namespace TemplateApi.Dominio.Escopos
{
    public class ConteudoEscp<T> : Comum.BaseEscopo<T>
        where T : ISelfValidation
    {
        public ConteudoEscp(T entidade)
            : base(entidade) { }

        public void IdEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.IntIsValid(expressao);
        }

        public void TituloEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void AliasEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void TextoEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
        }

        public void StatusEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EnumForStringIsValid(expressao, typeof(Status));
        }
    }
}
