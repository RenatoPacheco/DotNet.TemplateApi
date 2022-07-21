using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.Json.JsonConverte;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json
{
    public class ContratoJson : JsonNamingPolicy
    {
        private static JsonSerializerOptions _configuracao;
        public static JsonSerializerOptions Configuracao
        {
            get => _configuracao ??= Configurar(new JsonSerializerOptions());
        }

        public static JsonSerializerOptions Configurar(JsonSerializerOptions settings)
        {
            settings.PropertyNamingPolicy = new ContratoJson();
            settings.IgnoreNullValues = true;
            settings.Converters.Add(new JsonStringEnumConverter());
            settings.Converters.Add(new PhoneTypeJsonConverte());
            settings.Converters.Add(new IntInputJsonConverte());
            settings.Converters.Add(new LongInputJsonConverte());
            settings.Converters.Add(new DoubleInputJsonConverte());
            settings.Converters.Add(new DecimalInputJsonConverte());
            settings.Converters.Add(new GuidInputJsonConverte());
            settings.Converters.Add(new BoolInputJsonConverte());
            settings.Converters.Add(new DateTimeInputJsonConverte());
            settings.Converters.Add(new TimeSpanInputJsonConverte());
            settings.Converters.Add(new EnumInputJsonConverte<Status>());

            return settings;
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
            return name?.StartToLower();
        }
    }
}
