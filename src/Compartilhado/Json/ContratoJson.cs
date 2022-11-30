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
            get => _configuracao ??= ConfiguracaoJson.AplicarParaLeitura();
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
    }
}
