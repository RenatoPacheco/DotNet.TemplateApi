using System;

namespace TemplateApi.Compartilhados.Json.Notacoes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonIgnoreSerializeAttribute : Attribute
    {

    }
}
