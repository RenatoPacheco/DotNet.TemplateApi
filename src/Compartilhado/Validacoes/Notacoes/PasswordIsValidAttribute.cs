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
            ErrorMessageResourceName = nameof(AvisosResx.XSenhaDeveConter);
        }

        protected override bool Check(object value)
        {
            string input = Convert.ToString(value);
            bool result = false;

            if (!Regex.IsMatch(input, @"^\w\-_$%#@!*\.+=$"))
                if (Regex.IsMatch(input, @"[A-Z]"))
                    if (Regex.IsMatch(input, @"[a-z]"))
                        if (Regex.IsMatch(input, @"[0-9]"))
                            if (Regex.IsMatch(input, @"[\-_\$%#@!\*\.+=]"))
                                if (!Regex.IsMatch(input, @"0{3}|1{3}|2{3}|3{3}|4{3}|5{3}|6{3}|7{3}|8{3}|9{3}"))
                                    result = true;

            return result;
        }
    }
}
