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

        public static bool TryParseJson<T>(this string obj, out T result)
        {
            try
            {
                result = ContratoJson.Desserializar<T>(obj);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static T ParseJson<T>(this string obj)
        {
            return ContratoJson.Desserializar<T>(obj);
        }
    }
}
