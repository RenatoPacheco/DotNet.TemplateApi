using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCore.API.Template.Dominio.Extensoes;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Compartilhado.Json.JsonConverte
{
    public class LongInputDataJsonConverte : JsonConverter<LongInputData>
    {
        public override LongInputData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            LongInputData result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new LongInputData(reader.GetString());
            }
            else
            {
                result = new LongInputData(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, LongInputData value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
