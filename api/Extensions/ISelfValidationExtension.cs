using System;
using System.Linq;
using BitHelp.Core.Validation;
using TemplateApi.RecursoResx;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.Extensions
{
    public static class ISelfValidationExtension
    {
        public static void ExtrairModelState(this ISelfValidation entidade, ModelStateDictionary dados)
        {
            string chave, referencia;
            ModelErrorCollection erros;
            int errosTotal;
            int chavesTotal = dados.Keys.Count();

            for (int c = 0; c < chavesTotal; c++)
            {
                chave = dados.Keys.ElementAt(c);
                erros = dados[chave].Errors;
                referencia = Regex.Replace(chave, @"^[^\.]+\.", "");
                errosTotal = erros.Count();
                if (erros.Any())
                {
                    entidade.Notifications.RemoveAtReference(referencia);
                    for (int e = 0; e < errosTotal; e++)
                    {
                        entidade.Notifications.AddError(erros[e].ErrorMessage.ToString(), referencia);
                    }
                }
            }

            dados.Clear();
        }

        public static void ExtrairModelStateParaBody(this ISelfValidation entidade, ModelStateDictionary dados)
        {
            string chave, referencia;
            int chavesTotal = dados.Keys.Count();

            for (int c = 0; c < chavesTotal; c++)
            {
                chave = dados.Keys.ElementAt(c);
                if (dados[chave].Errors.Any())
                {
                    referencia = Regex.Replace(chave, @"^[^\.]+\.", "");
                    entidade.Notifications.RemoveAtReference(referencia);
                    entidade.Notifications.AddError(
                        string.Format(AvisosResx.ValorNoFormatoInvalido, chave), referencia);
                }
            }

            dados.Clear();
        }



        public static bool InputTypeEhValido<T>(this T entidade, Expression<Func<T, object>> expression, IInputType checar)
            where T : ISelfValidation
        {
            bool ehValido = checar?.IsValid() ?? false;

            if ((checar != null) && !ehValido)
                entidade.Notifications.AddError(expression);

            return ehValido;
        }

        public static bool InputTypeEhValido<T>(this T entidade, Expression<Func<T, object>> expression, IEnumerable<IInputType> checar)
            where T : ISelfValidation
        {
            bool ehValido = !(checar?.Any(x => !x.IsValid()) ?? true);

            if ((checar != null) && !ehValido)
                entidade.Notifications.AddError(expression);

            return ehValido;
        }
    }
}
