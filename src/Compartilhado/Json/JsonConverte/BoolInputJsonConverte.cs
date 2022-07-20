using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class BoolInputJsonConverte : JsonConverter<BoolInput>
    {
        public override BoolInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            BoolInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new BoolInput(reader.GetString());
            }
            else
            {
                result = new BoolInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, BoolInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
