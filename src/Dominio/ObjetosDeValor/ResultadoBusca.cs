using System;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DotNetCore.API.Template.Dominio.Auxiliares;

namespace DotNetCore.API.Template.Dominio.ObjetosDeValor
{
    public class ResultadoBusca
    {
        public ResultadoBusca()
        { 
        
        }

        public ResultadoBusca(int total, int maximo)
            : this()
        {
            TotalDeResultados = total;
            TotalDePaginas = maximo < 1 ? 1
                : total % maximo > 0 ? total / maximo + 1 : total / maximo;
        }

        [Display(Name = "Total de resultados")]
        public int? TotalDeResultados { get; set; }

        [Display(Name = "Total de páginas")]
        public int? TotalDePaginas { get; set; }

        [Display(Name = "Resultado da página atual")]
        public string ResultadosDaPaginaAtual { get; set; } = "[]";

        public string ParaJson()
        {
            StringBuilder resultado = new StringBuilder();

            resultado.Append("{");
            resultado.Append($"\"totalDeResultados\":{TotalDeResultados},");
            resultado.Append($"\"totalDePaginas\":{TotalDePaginas},");
            resultado.Append($"\"resultadosDaPaginaAtual\":{ResultadosDaPaginaAtual}");
            resultado.Append("}");

            return resultado.ToString();
        }

        public static ResultadoBusca<T> ParaObjeto<T>(string json)
            where T : class
        {
            return ContratoJson.Deserializar(json, typeof(ResultadoBusca<T>)) as ResultadoBusca<T>;
        }
    }

    public class ResultadoBusca<T>
        where T : class
    {
        public static explicit operator T[](ResultadoBusca<T> value)
        {
            return value.ResultadosDaPaginaAtual;
        }

        public static explicit operator T(ResultadoBusca<T> value)
        {
            return value.ResultadosDaPaginaAtual.FirstOrDefault();
        }

        public static string Vazio()
        {
            return ContratoJson.Serializar(new ResultadoBusca<T>());
        }

        [Display(Name = "Total de resultados")]
        public int TotalDeResultados { get; set; }

        [Display(Name = "Total de páginas")]
        public int TotalDePaginas { get; set; }

        [Display(Name = "Resultados da página atual")]
        public T[] ResultadosDaPaginaAtual { get; set; } = Array.Empty<T>();
    }
}
