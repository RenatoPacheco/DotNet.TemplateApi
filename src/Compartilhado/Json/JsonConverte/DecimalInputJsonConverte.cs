using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class DecimalInputJsonConverte : JsonConverter<DecimalInput>
    {
        public override DecimalInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DecimalInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new DecimalInput(reader.GetString());
            }
            else
            {
                result = new DecimalInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, DecimalInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
