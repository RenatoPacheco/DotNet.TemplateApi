using System;

namespace DotNetCore.API.Template.Dominio.Notacoes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class NaoRequerChavePublicaAttribute : Attribute
    {

    }
}