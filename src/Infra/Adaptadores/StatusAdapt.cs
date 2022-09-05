using System.Linq;
using System.Collections.Generic;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Infra.Adaptadores
{
    internal static class StatusAdapt
    {
        public static string EnumParaSql(Status? valor)
        {
            return valor is null ? null : ((int)valor).ToString();
        }

        public static string[] EnumParaSql(IEnumerable<Status> valor)
        {
            return valor.Select(x => EnumParaSql(x)).ToArray();
        }
    }
}
