using System;
using System.Text.Json;
using BitHelp.Core.Type.pt_BR;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Extensoes;

namespace TemplateApi.Compartilhado.Json.JsonConverte
{
    public class PhoneTypeJsonConverte : JsonConverter<PhoneType>
    {
        public override PhoneType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string result;

            if (reader.TokenType == JsonTokenType.String)
            {
                result = reader.GetString();
            }
            else
            {
                result = reader.GetBytesToString();
            }

            return new PhoneType(result);
        }

        public override void Write(Utf8JsonWriter writer, PhoneType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
