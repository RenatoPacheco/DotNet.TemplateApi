using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCore.API.Template.Compartilhado.Extensoes;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Compartilhado.Json.JsonConverte
{
    public class LongInputJsonConverte : JsonConverter<LongInput>
    {
        public override LongInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            LongInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new LongInput(reader.GetString());
            }
            else
            {
                result = new LongInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, LongInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
