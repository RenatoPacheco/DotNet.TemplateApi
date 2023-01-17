using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using TemplateApi.Compartilhado.Json.Notacoes;

namespace TemplateApi.Compartilhado.Json
{
    public class ContratoJson : DefaultContractResolver
    {
        public static string Serializar(object valor, JsonSerializerSettings settings = null)
        {
            return JsonConvert.SerializeObject(valor, ConfiguracaoJson.Leitura(settings));
        }

        public static T Desserializar<T>(string valor, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject<T>(valor, ConfiguracaoJson.Leitura(settings));
        }

        public static object Desserializar(string valor, Type tipo, JsonSerializerSettings settings = null)
        {
            return JsonConvert.DeserializeObject(valor, tipo, settings ?? ConfiguracaoJson.Leitura(settings));
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
                PropertyInfo property = actualProperties.FirstOrDefault(
                    x => x.Name.ToLower() == jsonProperty.PropertyName.ToLower());

                if (property != null)
                {
                    if (property.GetCustomAttribute(typeof(JsonIgnoreAttribute)) != null
                        || property.PropertyType == typeof(ValidationNotification))
                    {
                        jsonProperty.Ignored = true;
                    }
                    else
                    {
                        if (property.GetCustomAttribute(typeof(JsonIgnoreDeserializeAttribute)) != null)
                        {
                            jsonProperty.Writable = false;
                        }

                        if (property.GetCustomAttribute(typeof(JsonIgnoreSerializeAttribute)) != null)
                        {
                            jsonProperty.Readable = false;
                        }
                    }
                }
            }
            return properties;

        }
    }
}
