using System;
using BitHelp.Core.Validation;
using System.Linq.Expressions;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.Validacoes.Extensoes;

namespace DotNetCore.API.Template.Dominio.Escopos
{
    public class ConteudoEscp<TClasse> : BaseEscp<TClasse>
        where TClasse : ISelfValidation
    {
        public ConteudoEscp(TClasse entidade)
            : base(entidade) { }

        public void IdEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.IntIsValid(expressao);
        }

        public void TituloEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void AliasEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void TextoEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
        }

        public void StatusEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EnumForStringIsValid(expressao, typeof(Status));
        }
    }
}
