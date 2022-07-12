using System;
using BitHelp.Core.Security;
using System.Text.RegularExpressions;

namespace DotNetCore.API.Template.Compartilhado.Seguranca
{
    public class Codificacao
    {
        private static readonly string Chave = "DotNetCore.API.Template";
        private static readonly string Senha = "74Ixdj9AnA5d1cTP";

        public static string Encriptar(string value)
        {
            return Encripty.Crypt(value, Senha, Chave);
        }
        public static string Encriptar(string value, DateTime timeout)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            value = $"{timeout.Year}-{timeout.Month}-{timeout.Day}|{value}";
            return Encripty.Crypt(value, Senha, Chave);
        }

        public static string Decriptar(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            string expressao = @"^([^\|]+)(\|)(.*)$";
            string resultado = Encripty.Decrypt(value, Senha, Chave) ?? string.Empty;
            bool dataLimite = Regex.IsMatch(resultado, expressao);
            DateTime data = dataLimite ? Convert.ToDateTime(Regex.Replace(resultado, @"^([^\|]+)(\|)(.*)$", "$1")) : new DateTime();

            if (data != new DateTime() && DateTime.Compare(DateTime.Parse(DateTime.Now.ToShortDateString()), data) > 0)
            {
                resultado = string.Empty;
            }
            else if (dataLimite)
            {
                resultado = Regex.Replace(resultado, expressao, "$3");
            }

            return resultado;
        }
    }
}