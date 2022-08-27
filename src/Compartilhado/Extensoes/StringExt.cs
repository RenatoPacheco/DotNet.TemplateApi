using System.Text.RegularExpressions;
using TemplateApi.Compartilhado.Json;

namespace TemplateApi.Compartilhado.Extensoes
{
    public static class StringExt
    {
        public static string StartToLower(this string source)
        {
            if (!string.IsNullOrEmpty(source) && char.IsUpper(source[0]))
                source = $"{char.ToLower(source[0])}{source[1..]}";

            return source;
        }

        public static string StartToUpper(this string source)
        {
            if (!string.IsNullOrEmpty(source) && !char.IsUpper(source[0]))
                source = $"{char.ToUpper(source[0])}{source[1..]}";

            return source;
        }

        public static string ToJsonReference(this string source)
        {
            if (!string.IsNullOrEmpty(source))
                source = Regex.Replace(source, @"^.|\..", (v) =>
                {
                    return v.Value.ToLower();
                });

            return source;
        }

        public static bool TryParseJson<T>(this string source, out T output)
        {
            try
            {
                output = ContratoJson.Desserializar<T>(source);
                return true;
            }
            catch
            {
                output = default;
                return false;
            }
        }

        public static T ParseJson<T>(this string source)
        {
            return ContratoJson.Desserializar<T>(source);
        }
    }
}
