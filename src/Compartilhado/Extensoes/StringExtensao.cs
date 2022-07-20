namespace TemplateApi.Compartilhado.Extensoes
{
    public static class StringExtensao
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
    }
}
