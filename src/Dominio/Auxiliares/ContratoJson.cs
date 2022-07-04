﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNetCore.API.Template.Dominio.Auxiliares
{
    public class ContratoJson : JsonNamingPolicy
    {
        private static JsonSerializerOptions _configuracao;
        public static JsonSerializerOptions Configuracao
        {
            get => _configuracao ??= Configurar(new JsonSerializerOptions());
        }

        public static JsonSerializerOptions Configurar(JsonSerializerOptions settings)
        {
            settings.PropertyNamingPolicy = new ContratoJson();
            settings.IgnoreNullValues = true;
            settings.Converters.Add(new JsonStringEnumConverter());

            return settings;
        }

        public static string Serializar<T>(T value) 
            where T : class
        {
            return JsonSerializer.Serialize(value, Configuracao);
        }

        public static object Deserializar(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, Configuracao);
        }

        public override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
                return name;

            return $"{char.ToLower(name[0])}{name[1..]}";
        }
    }
}
