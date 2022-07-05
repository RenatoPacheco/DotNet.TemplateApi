using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace DotNetCore.API.Template.Repositorio.Adaptadores
{
    internal static class CharAdapt
    {
        public static string SqlParaBoolean(string campo)
        {
            StringBuilder resultado = new StringBuilder();

            resultado.Append($" CASE {campo} ");
            resultado.Append($" WHEN '1' THEN 'true' ");
            resultado.Append($" ELSE 'false' END ");

            return resultado.ToString();
        }

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
