using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class EnumInputJsonConverte<T> : JsonConverter<EnumInput<T>>
        where T : struct
    {
        public override EnumInput<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            EnumInput<T> result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = new EnumInput<T>(reader.GetString());
            }
            else
            {
                result = new EnumInput<T>(reader.GetBytesToString());
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, EnumInput<T> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
