using System.Linq;
using System.Collections.Generic;

namespace TemplateApi.Infra.Adaptadores
{
    internal static class CharAdapt
    {
        public static string BooleanParaSql(bool? valor)
        {
            return valor is null ? null : valor.Value ? "1" : "0";
        }

        public static string[] BooleanParaSql(IEnumerable<bool> valor)
        {
            return valor.Select(x => BooleanParaSql(x)).ToArray();
        }
    }
}
