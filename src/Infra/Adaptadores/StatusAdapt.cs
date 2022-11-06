using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Infra.Adaptadores
{
    internal static class StatusAdapt
    {
        public static string SqlParaEnum(string campo)
        {
            StringBuilder resultado = new StringBuilder();
            Status[] opcoes = Enum.GetValues(typeof(Status)).Cast<Status>().ToArray();
            
            resultado.Append(" CASE ");
            foreach(Status item in opcoes)
            {
                resultado.Append($" WHEN {campo} = '{(int)item}' THEN '{item}' ");
            }
            resultado.Append(" ELSE NULL  ");

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
