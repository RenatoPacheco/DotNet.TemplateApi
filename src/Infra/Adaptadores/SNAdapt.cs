using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace TemplateApi.Infra.Adaptadores
{
    internal static class SNAdapt
    {
        public static string SqlParaBoolean(string campo)
        {
            StringBuilder resultado = new StringBuilder();

            resultado.Append($" CASE ");
            resultado.Append($" WHEN {campo} IN ('S','1') THEN 'true' ");
            resultado.Append($" ELSE 'false' END ");

            return resultado.ToString();
        }

        public static string BooleanParaSql(bool? valor)
        {
            return valor is null ? null : valor.Value ? "S" : "N";
        }

        public static string[] BooleanParaSql(IEnumerable<bool> valor)
        {
            return valor.Select(x => BooleanParaSql(x)).ToArray();
        }
    }
}
