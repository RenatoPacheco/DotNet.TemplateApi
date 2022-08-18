using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using BitHelp.Core.Validation;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.Comum;
using TemplateApi.Compartilhado.Json.JsonConverte;

namespace TemplateApi.Compartilhado.Json
{
    public class ContratoJson : DefaultContractResolver
    {
        private static JsonSerializerSettings _configuracao;
        public static JsonSerializerSettings Configuracao
        {
            get => _configuracao ??= Configurar(new JsonSerializerSettings());
        }

        public static JsonSerializerSettings Configurar(JsonSerializerSettings settings)
        {
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.Culture = System.Globalization.CultureInfo.CurrentCulture;
            settings.ContractResolver = new ContratoJson();

            settings.Converters.Add(new StringEnumConverter());
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

        public static string Serializar<T>(T value) 
            where T : class
        {
            return JsonConvert.SerializeObject(value, Configuracao);
        }

        public static object Desserializar(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type, Configuracao);
        }

        public static T Desserializar<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Configuracao);
        }

        public ContratoJson()
            : base()
        {
            // Aplicar o comportamento de CamelCasePropertyNamesContractResolver
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = true,
                OverrideSpecifiedNames = true
            };
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            PropertyInfo[] actualProperties = type.GetProperties();

            foreach (JsonProperty jsonProperty in properties)
            {
                PropertyInfo property = actualProperties.FirstOrDefault(x => x.Name.ToLower() == jsonProperty.PropertyName.ToLower());
                if (property != null && (property.GetCustomAttribute(typeof(JsonIgnoreAttribute)) != null
                    || property.PropertyType == typeof(ValidationNotification)))
                {
                    jsonProperty.Ignored = true;
                }
            }
            return properties;

        }
    }
}
