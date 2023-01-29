using System;
using Newtonsoft.Json;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class DecimalJsonConverte
        : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal)
                || objectType == typeof(decimal?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader?.Value?.ToString();
            return value is null ? null : DecimalInput.TryParse(value, out DecimalInput v) ? (decimal)v : (decimal?)null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string v = (new DecimalInput(value as decimal?))?.ToString();

            if (v is null)
                writer.WriteNull();
            else
                writer.WriteValue(v);
        }
    }
}