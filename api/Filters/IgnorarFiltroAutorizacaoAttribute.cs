using System;

namespace TemplateApi.Api.Filters
{
    /// <summary>
    /// Esse filtro é para quando quer que a action controle o resultado de erro quando não autorizado
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class IgnorarFiltroAutorizacaoAttribute : Attribute
    {

    }
}