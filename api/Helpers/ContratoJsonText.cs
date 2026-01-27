using System.Text.Json;
using TemplateApi.Compartilhados.Extensoes;

namespace TemplateApi.Api.Helpers {
    public class ContratoJsonText : JsonNamingPolicy {
        private static JsonSerializerOptions _configuracao;
        private static JsonSerializerOptions Configuracao {
            get => _configuracao ??= ConfiguracaoJsonText.Leitura();
        }

        public static string Serializar<T>(T value)
            where T : class {
            return JsonSerializer.Serialize(value, Configuracao);
        }

        public static object Desserializar(string json, Type type) {
            return JsonSerializer.Deserialize(json, type, Configuracao);
        }

        public static T Desserializar<T>(string json) {
            return JsonSerializer.Deserialize<T>(json, Configuracao);
        }

        public override string ConvertName(string name) {
            return name?.ToJsonReference();
        }
    }
}