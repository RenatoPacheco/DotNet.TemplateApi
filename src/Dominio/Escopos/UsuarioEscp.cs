using System;
using BitHelp.Core.Validation;
using System.Linq.Expressions;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.Validacoes.Extensoes;

namespace DotNetCore.API.Template.Dominio.Escopos
{
    public class UsuarioEscp<TClasse> : BaseEscp<TClasse>
        where TClasse : ISelfValidation
    {
        public UsuarioEscp(TClasse entidade)
            : base(entidade) { }

        public void IdEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.IntIsValid(expressao);
        }

        public void NomeEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void EmailEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EmailIsValid(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void TelefoneEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.PhoneTypeIsValid(expressao);
            _entidade.MaxCharactersIsValid(expressao, 50);
        }

        public void StatusEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EnumForStringIsValid(expressao, typeof(Status));
        }
    }
}
