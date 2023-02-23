using System;
using Newtonsoft.Json;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class FloatJsonConverte
        : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(float)
                || objectType == typeof(float?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string value = reader?.Value?.ToString();
            return value is null ? null : FloatInput.TryParse(value, out FloatInput v) ? (float)v : (float?)null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            FloatInput v = value is float output ? (FloatInput)output : null;

            if (v is null)
            {
                writer.WriteNull();
            }
            else
            {
                if (v.ToString().IndexOf(".") >= 0)
                {
                    writer.WriteValue((float)v);
                }
                else
                {
                    writer.WriteRawValue(v?.ToString());
                }
            }
        }
    }
}