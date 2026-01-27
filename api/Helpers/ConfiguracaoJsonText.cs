using System.Text.Json;
using System.Text.Json.Serialization;

namespace TemplateApi.Api.Helpers {
    public static class ConfiguracaoJsonText {
        private static JsonSerializerOptions Base(JsonSerializerOptions settings) {
            settings.PropertyNamingPolicy = new ContratoJsonText();
            settings.Converters.Add(new JsonStringEnumConverter());

            return settings;
        }

        public static JsonSerializerOptions Leitura(JsonSerializerOptions settings = null) {
            settings = Base(settings ?? new JsonSerializerOptions());

            return settings;
        }

        public static JsonSerializerOptions Escrita(JsonSerializerOptions settings = null) {
            settings = Base(settings ?? new JsonSerializerOptions());
            settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            return settings;
        }
    }
}