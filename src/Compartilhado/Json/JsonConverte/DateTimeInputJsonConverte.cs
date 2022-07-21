using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class DateTimeInputJsonConverte : JsonConverter<DateTimeInput>
    {
        public override DateTimeInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTimeInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new DateTimeInput(reader.GetString());
            }
            else
            {
                result = new DateTimeInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
