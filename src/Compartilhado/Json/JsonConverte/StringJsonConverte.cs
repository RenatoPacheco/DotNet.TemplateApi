using System;
using Newtonsoft.Json;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class StringJsonConverte
        : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader?.Value?.ToString()?.Trim();
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string v = value?.ToString()?.Trim();

            if (v is null)
                writer.WriteNull();
            else
                writer.WriteValue(v);
        }
    }
}