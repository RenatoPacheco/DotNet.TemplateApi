using System.Linq;
using System.Text;
using System.Collections.Generic;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Repositorio.Adaptadores
{
    internal static class StatusAdapt
    {
        public static string SqlParaEnum(string campo)
        {
            StringBuilder resultado = new StringBuilder();

            resultado.Append($" CASE {campo} ");
            resultado.Append($" WHEN '1' THEN '{Status.Ativo}' ");
            resultado.Append($" ELSE CASE {campo} ");
            resultado.Append($" WHEN '2' THEN '{Status.Excluido}' ");
            resultado.Append($" ELSE '{Status.Inativo}' END END ");

            return resultado.ToString();
        }

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
