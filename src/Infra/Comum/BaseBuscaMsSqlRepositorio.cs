using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Infra.Comum
{
    internal abstract class BaseBuscaMsSqlRepositorio
        : BaseRepositorio
    {
        /// <summary>
        /// Tratar o texto para a busca 
        /// </summary>
        public IList<string> DesmebrarTexto(string tratar, int minimo = 2)
        {
            List<string> resultado = new List<string>();

            if (string.IsNullOrWhiteSpace(tratar))
            {
                return resultado;
            }

            string texto = tratar;
            texto = Regex.Replace(texto, @"[`|'|;]", " ").Trim();
            texto = Regex.Replace(texto, @"[ ]{2,}", " ").Trim();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                MatchCollection agrupar = Regex.Matches(texto, "(\"[^\"]+\")", RegexOptions.IgnoreCase);
                if (agrupar.Count > 0)
                {
                    foreach (Match item in agrupar)
                    {
                        texto = texto.Replace(item.Groups[1].Value, "");
                        string novo = item.Groups[1].Value.Replace("\"", "");
                        resultado.Add(novo);

                    }
                    texto = Regex.Replace(texto, @"[`|'|;]", " ").Trim();
                    texto = Regex.Replace(texto, @"[ ]{2,}", " ").Trim();
                }
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    string[] separar = texto.Split(' ');
                    resultado.Add(texto);
                    foreach (string item in separar)
                    {
                        if (item.Length > 0)
                        {
                            resultado.Add(item);
                        }
                    }
                }
            }

            return resultado.Where(x => x.Length > minimo).Distinct().ToList();
        }

        /// <summary>
        /// Só funciona se for adicionado após um order by
        /// </summary>
        public string MontarPaginacao(FiltrarBaseCmd comando)
        {
            if (comando.Maximo > 0 && comando.Maximo < int.MaxValue)
            {
                int pagina = comando.Pagina < 1 ? 1 : comando.Pagina;
                return comando.Maximo < 1 ? string.Empty : $" OFFSET {(pagina - 1) * comando.Maximo} ROWS FETCH FIRST {comando.Maximo}  ROWS ONLY ";
            }
            else if (comando.Maximo > 0 && comando.Maximo < int.MaxValue)
            {
                return $" OFFSET 0 ROWS FETCH FIRST {comando.Maximo}  ROWS ONLY ";
            }

            return string.Empty;
        }
    }
}
