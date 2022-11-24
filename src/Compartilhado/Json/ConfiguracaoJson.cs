using System.Text.Json;
using System.Text.Json.Serialization;
using TemplateApi.Compartilhado.Json.JsonConverte;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Compartilhado.Json
{
    public static class ConfiguracaoJson
    {
        private static JsonSerializerOptions AplicarBase(JsonSerializerOptions settings)
        {
            settings.PropertyNamingPolicy = new ContratoJson();
            settings.Converters.Add(new JsonStringEnumConverter());
            settings.Converters.Add(new PhoneTypeJsonConverte());
            settings.Converters.Add(new IntInputJsonConverte());
            settings.Converters.Add(new LongInputJsonConverte());
            settings.Converters.Add(new DoubleInputJsonConverte());
            settings.Converters.Add(new DecimalInputJsonConverte());
            settings.Converters.Add(new GuidInputJsonConverte());
            settings.Converters.Add(new BoolInputJsonConverte());
            settings.Converters.Add(new DateTimeInputJsonConverte());
            settings.Converters.Add(new TimeSpanInputJsonConverte());
            settings.Converters.Add(new EnumInputJsonConverte<Status>());
            settings.Converters.Add(new EnumInputJsonConverte<ContextoCmd>());

            return settings;
        }

        public static JsonSerializerOptions AplicarParaLeitura(JsonSerializerOptions settings = null)
        {
            settings = AplicarBase(settings ?? new JsonSerializerOptions());

            return settings;
        }

        public static JsonSerializerOptions AplicarParaEscrita(JsonSerializerOptions settings = null)
        {
            settings = AplicarBase(settings ?? new JsonSerializerOptions());
            settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            return settings;
        }
    }
}