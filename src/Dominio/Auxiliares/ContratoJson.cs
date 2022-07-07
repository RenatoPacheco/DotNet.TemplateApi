using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCore.API.Template.Dominio.Auxiliares.JsonConverte;
using DotNetCore.API.Template.Dominio.Extensoes;

namespace DotNetCore.API.Template.Dominio.Auxiliares
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
