using System;
using System.Text.RegularExpressions;
using BitHelp.Core.Validation.Notations;
using DotNetCore.API.Template.Recurso;

namespace DotNetCore.API.Template.Compartilhado.Validacoes.Notacoes
{
    [AttributeUsage(AttributeTargets.Property |
           AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordIsValidAttribute : ListIsValidAttribute
    {
        public PasswordIsValidAttribute() : base()
        {
            ErrorMessageResourceType = typeof(AvisosResx);
            ErrorMessageResourceName = nameof(AvisosResx.SenhaDeveConter);
        }

        protected override bool Check(object value)
        {
            string input = Convert.ToString(value);
            bool result = false;

            if (!Regex.IsMatch(input, @"^\w\-_$%#@!*\.+=\{\}$"))
                if (Regex.IsMatch(input, @"[A-Z]"))
                    if (Regex.IsMatch(input, @"[a-z]"))
                        if (Regex.IsMatch(input, @"[0-9]"))
                            if (Regex.IsMatch(input, @"\-_$%#@!*\.+=\{\}"))
                                if (!Regex.IsMatch(input, @"0{2}|1{2}|2{2}|3{2}|4{2}|5{2}|6{2}|7{2}|8{2}|9{2}"))
                                    result = true;

            return result;
        }
    }
}
