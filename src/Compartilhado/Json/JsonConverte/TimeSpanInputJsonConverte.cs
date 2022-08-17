using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class TimeSpanInputJsonConverte : JsonConverter<TimeSpanInput>
    {
        public override TimeSpanInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TimeSpanInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new TimeSpanInput(reader.GetString());
            }
            else
            {
                result = new TimeSpanInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpanInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
