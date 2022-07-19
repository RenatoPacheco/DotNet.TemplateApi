using BitHelp.Core.Validation;
using DotNetCore.API.Template.RecursoResx;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using BitHelp.Core.Validation.Extends;
using System.Text.RegularExpressions;

namespace DotNetCore.API.Template.Site.Extensions
{
    public static class IAutoValidacaoExtensions
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
    }
}
