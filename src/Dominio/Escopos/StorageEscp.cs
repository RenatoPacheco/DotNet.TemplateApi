﻿using System;
using BitHelp.Core.Validation;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using DotNetCore.API.Template.Recurso;
using BitHelp.Core.Validation.Extends;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.Validacoes.Extensoes;

namespace DotNetCore.API.Template.Dominio.Escopos
{
    public class StorageEscp<TClasse> : BaseEscp<TClasse>
        where TClasse : ISelfValidation
    {
        public StorageEscp(TClasse entidade)
            : base(entidade) { }

        public void IdEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.LongIsValid(expressao);
        }

        public void NomeEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void DiretorioEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void ExtensaoEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 50);
            _entidade.RegexIsValid(expressao, @"^(\.)(jpg|jpeg|png|doc|docx|xls|xlsx|txt|xml|pdf)$", RegexOptions.IgnoreCase)
                    .SetMessage(string.Format(AvisosResx.TipoDeArquivoStorageInvalido));
        }

        public void TipoEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 50);
        }

        public void PesoEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.LongIsValid(expressao);
            _entidade.MaxNumberIsValid(expressao, 100 * 1024)
                .SetMessage(string.Format(AvisosResx.PesoDoArquivoEmKbInvalido, (100 * 1042)));
        }

        public void ReferenciaEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.MaxCharactersIsValid(expressao, 255);
        }

        public void StatusEhValido(Expression<Func<TClasse, object>> expressao)
        {
            _entidade.RemoveAtReference(expressao);
            _entidade.EnumForStringIsValid(expressao, typeof(Status));
        }
    }
}
