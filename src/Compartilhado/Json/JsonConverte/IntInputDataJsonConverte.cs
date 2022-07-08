using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCore.API.Template.Dominio.Extensoes;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Compartilhado.Json.JsonConverte
{
    public class IntInputDataJsonConverte : JsonConverter<IntInputData>
    {
        public override IntInputData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            IntInputData result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new IntInputData(reader.GetString());
            }
            else
            {
                result = new IntInputData(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, IntInputData value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
