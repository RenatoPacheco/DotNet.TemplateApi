using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.Json.JsonConverte;

namespace TemplateApi.Compartilhado.Json
{
    public static class ConfiguracaoJson
    {
        private static JsonSerializerSettings AplicarBase(JsonSerializerSettings settings)
        {
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Culture = System.Globalization.CultureInfo.CurrentCulture;
            settings.ContractResolver = new ContratoJson();

            settings.Converters.Add(new StringEnumConverter());
            settings.Converters.Add(new PhoneTypeJsonConverte());
            settings.Converters.Add(new IntInputJsonConverte());
            settings.Converters.Add(new LongInputJsonConverte());
            settings.Converters.Add(new FloatInputJsonConverte());
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

        public static JsonSerializerSettings AplicarParaLeitura(JsonSerializerSettings settings = null)
        {
            settings = AplicarBase(settings ?? new JsonSerializerSettings());

            return settings;
        }

        public static JsonSerializerSettings AplicarParaEscrita(JsonSerializerSettings settings = null)
        {
            settings = AplicarBase(settings ?? new JsonSerializerSettings());
            settings.NullValueHandling = NullValueHandling.Ignore;

            return settings;
        }
    }
}
