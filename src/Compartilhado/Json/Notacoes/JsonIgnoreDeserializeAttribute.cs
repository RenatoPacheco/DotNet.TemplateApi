using System;

namespace TemplateApi.Compartilhado.Json.Notacoes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonIgnoreDeserializeAttribute : Attribute
    {

    }
}
