using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class DoubleInputJsonConverte : JsonConverter<DoubleInput>
    {
        public override DoubleInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DoubleInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new DoubleInput(reader.GetString());
            }
            else
            {
                result = new DoubleInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DoubleInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
