using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace TemplateApi.Compartilhado.Json
{
    public class ContratoJson : DefaultContractResolver
    {
        private static JsonSerializerSettings _configuracao;
        private static JsonSerializerSettings Configuracao
        {
            get => _configuracao ??= ConfiguracaoJson.AplicarParaLeitura(new JsonSerializerSettings());
        }

        public static string Serializar<T>(T value, JsonSerializerSettings settings = null)
            where T : class
        {
            return JsonConvert.SerializeObject(value, settings ?? Configuracao);
        }

        public static object Desserializar(string json, Type type, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject(json, type, settings ?? Configuracao);
        }

        public static T Desserializar<T>(string json, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(json, settings ?? Configuracao);
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
