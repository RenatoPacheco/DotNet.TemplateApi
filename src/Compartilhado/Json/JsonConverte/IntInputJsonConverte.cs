using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class IntInputJsonConverte : JsonConverter<IntInput>
    {
        public override IntInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IntInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new IntInput(reader.GetString());
            }
            else
            {
                result = new IntInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, IntInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
