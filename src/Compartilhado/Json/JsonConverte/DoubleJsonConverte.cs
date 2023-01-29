using System;
using Newtonsoft.Json;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class DoubleJsonConverte
        : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double)
                || objectType == typeof(double?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader?.Value?.ToString();
            return value is null ? null : DoubleInput.TryParse(value, out DoubleInput v) ? (double)v : (double?)null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string v = (new DoubleInput(value as double?))?.ToString();

            if (v is null)
                writer.WriteNull();
            else
                writer.WriteValue(v);
        }
    }
}