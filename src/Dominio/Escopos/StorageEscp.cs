using System;
using BitHelp.Core.Validation;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using TemplateApi.RecursoResx;
using BitHelp.Core.Validation.Extends;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.Validacoes.Extensoes;

namespace TemplateApi.Dominio.Escopos
{
    public class StorageEscp<T>
        where T : ISelfValidation
    {
        public StorageEscp(T entidade)
        {
            _entidade = entidade;
        }

        protected readonly T _entidade;

        public void IdEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.LongIsValid(expressao);
        }

        public void NomeEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void AliasEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void DiretorioEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }


        public void ChecksumEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 1000);
        }

        public void ExtensaoEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 50);
            _entidade.RegexIsValid(expressao, @"^(\.)(jpg|jpeg|png|doc|docx|xls|xlsx|txt|xml|pdf)$", RegexOptions.IgnoreCase)
                    .SetMessage(string.Format(AvisosResx.TipoDeArquivoStorageInvalido));
        }

        public void TipoEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 50);
        }

        public void PesoEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.LongIsValid(expressao);
            _entidade.MaxNumberIsValid(expressao, 100 * 1024)
                .SetMessage(string.Format(AvisosResx.PesoDoArquivoEmKbInvalido, (100 * 1042)));
        }

        public void ReferenciaEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void StatusEhValido(Expression<Func<T, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EnumForStringIsValid(expressao, typeof(Status));
        }
    }
}
