using System;

namespace TemplateApi.Dominio.Notacoes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AcessoLivreAttribute : Attribute
    {

    }
}