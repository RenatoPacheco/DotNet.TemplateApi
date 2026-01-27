using Newtonsoft.Json;

namespace TemplateApi.Dominio.Json {
    public static class ConverterJson {

        public static string Serializar(object valor, JsonSerializerSettings settings = null) {
            return JsonConvert.SerializeObject(valor, ConfiguracaoJson.Leitura(settings));
        }

        public static T Desserializar<T>(string valor, JsonSerializerSettings settings = null) {
            return JsonConvert.DeserializeObject<T>(valor, ConfiguracaoJson.Leitura(settings));
        }

        public static object Desserializar(string valor, Type tipo, JsonSerializerSettings settings = null) {
            return JsonConvert.DeserializeObject(valor, tipo, settings ?? ConfiguracaoJson.Leitura(settings));
        }

    }
}
