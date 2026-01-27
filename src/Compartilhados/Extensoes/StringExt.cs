using System.Text;
using System.Text.RegularExpressions;
using TemplateApi.Dominio.Json;

namespace TemplateApi.Compartilhados.Extensoes {
    public static class StringExt {

        public static string ToBase64(this string value) {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static bool TryDecodeBase64(this string value, out byte[] bytes) {
            bytes = null;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();

            if (value.Length % 4 != 0)
                return false;

            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,2}$"))
                return false;

            try {
                bytes = Convert.FromBase64String(value);
                return true;
            } catch (FormatException) {
                return false;
            }
        }

        public static string StartToLower(this string source) {
            if (!string.IsNullOrEmpty(source) && char.IsUpper(source[0])) {
                if (source.Length == 1) {
                    source = $"{char.ToLower(source[0])}";
                } else {
                    source = $"{char.ToLower(source[0])}{source.Substring(1)}";
                }
            }

            return source;
        }

        public static string StartToUpper(this string source) {
            if (!string.IsNullOrEmpty(source) && !char.IsUpper(source[0])) {
                if (source.Length == 1) {
                    source = $"{char.ToUpper(source[0])}";
                } else {
                    source = $"{char.ToUpper(source[0])}{source.Substring(1)}";
                }
            }

            return source;
        }

        public static string ToJsonReference(this string source) {
            if (!string.IsNullOrEmpty(source))
                source = Regex.Replace(source, @"^.|\..", (v) => {
                    return v.Value.ToLower();
                });

            return source;
        }

        public static T ParseJson<T>(this string source) {
            return ConverterJson.Desserializar<T>(source);
        }

        public static string HideEmail(this string source) {
            string result = Regex.Replace(source, @"([^@]{1,3})([^@]*)(@)", "$1****$3");
            return Regex.Replace(result, @"(@)([^@]{1,3})([^@]*)", "$1$2****");
        }
    }
}
