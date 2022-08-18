using System;
using Newtonsoft.Json;
using BitHelp.Core.Type.pt_BR;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class PhoneTypeJsonConverte : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PhoneType)
                || objectType == typeof(PhoneType?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader?.Value?.ToString();
            if (objectType == typeof(PhoneType?) && value is null)
            {
                return null;
            }

            return new PhoneType(value);
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
