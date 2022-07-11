using System;

namespace DotNetCore.API.Template.Dominio.Notacoes
{
    /// <summary>
    /// Não precisa estar autenticado, mas precisa enviar a chave pública
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class AcessoBasicoAttribute : Attribute
    {

    }
}