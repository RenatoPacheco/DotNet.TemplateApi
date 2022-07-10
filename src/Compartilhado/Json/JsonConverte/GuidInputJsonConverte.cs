using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCore.API.Template.Compartilhado.Extensoes;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Compartilhado.Json.JsonConverte
{
    public class GuidInputJsonConverte : JsonConverter<GuidInput>
    {
        public override GuidInput Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            GuidInput result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new GuidInput(reader.GetString());
            }
            else
            {
                result = new GuidInput(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, GuidInput value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
