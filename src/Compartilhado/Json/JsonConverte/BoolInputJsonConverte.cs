using System;
using Newtonsoft.Json;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class BoolInputJsonConverte : JsonConverter
    {
        public override bool CanConvert(Type type)
        {
            return type == typeof(BoolInput);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader?.Value?.ToString();
            if (value is null)
            {
                return null;
            }

            return new BoolInput(value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string result = value?.ToString();

            if (result is null)
                writer.WriteNull();
            else
                writer.WriteValue(result);
        }
    }
}
