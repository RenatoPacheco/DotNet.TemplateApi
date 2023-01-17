using System;

namespace TemplateApi.Compartilhado.Json.Notacoes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class JsonIgnoreSerializeAttribute : Attribute
    {

    }
}
