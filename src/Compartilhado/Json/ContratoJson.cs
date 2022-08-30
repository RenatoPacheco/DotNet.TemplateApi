using System.Text.Json;
using TemplateApi.Compartilhado.Extensoes;

namespace TemplateApi.Compartilhado.Json
{
    public class ContratoJson : JsonNamingPolicy
    {
        private static JsonSerializerOptions _configuracao;
        private static JsonSerializerOptions Configuracao
        {
            get => _configuracao ??= ConfiguracaoJson.AplicarParaLeitura();
        }

        public static string Serializar<T>(T value) 
            where T : class
        {
            return JsonSerializer.Serialize(value, Configuracao);
        }

        public static object Desserializar(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, Configuracao);
        }

        public static T Desserializar<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Configuracao);
        }

        public override string ConvertName(string name)
        {
            return name?.ToJsonReference();
        }
    }
}
