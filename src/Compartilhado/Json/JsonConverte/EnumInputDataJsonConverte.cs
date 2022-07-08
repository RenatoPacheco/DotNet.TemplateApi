using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DotNetCore.API.Template.Dominio.Extensoes;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;

namespace DotNetCore.API.Template.Compartilhado.Json.JsonConverte
{
    public class EnumInputDataJsonConverte<T> : JsonConverter<EnumInputData<T>>
        where T : struct
    {
        public override EnumInputData<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            EnumInputData<T> result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new EnumInputData<T>(reader.GetString());
            }
            else
            {
                result = new EnumInputData<T>(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, EnumInputData<T> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
