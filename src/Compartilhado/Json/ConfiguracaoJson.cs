using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.Json.JsonConverte;

namespace TemplateApi.Compartilhado.Json
{
    public static class ConfiguracaoJson
    {
        private static JsonSerializerSettings Base(JsonSerializerSettings config = null)
        {
            JsonSerializerSettings resultado = config 
                ?? new JsonSerializerSettings();

            resultado.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            resultado.Culture = System.Globalization.CultureInfo.CurrentCulture;
            resultado.ContractResolver = new ContratoJson();

            resultado.Converters.Add(new StringEnumConverter());
            resultado.Converters.Add(new PhoneTypeJsonConverte());
            resultado.Converters.Add(new IntInputJsonConverte());
            resultado.Converters.Add(new LongInputJsonConverte());
            resultado.Converters.Add(new FloatInputJsonConverte());
            resultado.Converters.Add(new DoubleInputJsonConverte());
            resultado.Converters.Add(new DecimalInputJsonConverte());
            resultado.Converters.Add(new GuidInputJsonConverte());
            resultado.Converters.Add(new BoolInputJsonConverte());
            resultado.Converters.Add(new DateTimeInputJsonConverte());
            resultado.Converters.Add(new TimeSpanInputJsonConverte());
            resultado.Converters.Add(new EnumInputJsonConverte<Status>());
            resultado.Converters.Add(new EnumInputJsonConverte<ContextoCmd>());

            return resultado;
        }

        /// <summary>
        /// Essa é a configuração padrão se nada for definido
        /// </summary>
        public static JsonSerializerSettings Leitura(JsonSerializerSettings config = null)
        {
            JsonSerializerSettings resultado = Base(config);
            return resultado;
        }

        /// <summary>
        /// Essa é a configuração padrão se nada for definido
        /// </summary>
        public static JsonSerializerSettings Escrita(JsonSerializerSettings config = null)
        {
            JsonSerializerSettings resultado = Base(config);
            resultado.NullValueHandling = NullValueHandling.Ignore;
            return resultado;
        }
    }
}
