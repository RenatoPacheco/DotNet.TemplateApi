using BitHelp.Core.Validation;
using System.Linq.Expressions;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.Validacoes.Extensoes;

namespace TemplateApi.Dominio.Escopos
{
    public class UsuarioEscp<T>
        where T : ISelfValidation
    {
        public UsuarioEscp(T entidade)
        {
            _entidade = entidade;
        }

        protected readonly T _entidade;

        public void IdEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.IntIsValid(expressao);
        }

        public void NomeEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void EmailEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EmailIsValid(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void SenhaEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.RangeCharactersIsValid(expressao, 8, 30);
            _entidade.PasswordIsValid(expressao);
        }

        public void TelefoneEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.PhoneTypeIsValid(expressao);
            _entidade.MaxCharactersIsValid(expressao, 50);
        }

        public void StatusEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EnumForStringIsValid(expressao, typeof(Status));
        }
    }
}
