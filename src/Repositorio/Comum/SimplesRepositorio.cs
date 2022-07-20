using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Repositorio.Auxiliares;

namespace TemplateApi.Repositorio.Comum
{
    internal abstract class SimplesRepositorio 
        : BaseRepositorio
    {
        public SimplesRepositorio(
            Conexao conexao,
            IUnidadeTrabalho udt)
        {
            Conexao = conexao;
            _udt = udt;
        }

        private readonly IUnidadeTrabalho _udt;

        protected Conexao Conexao { get; private set; }

        public IniciarTransicao IniciarTransicao()
        {
            return new IniciarTransicao(_udt, this);
        }

        /// <summary>
        /// Só funciona se for adicionado após um order by
        /// </summary>
        public string MontarPaginacao(int pagina, int maximo)
        {
            pagina = pagina < 1 ? 1 : pagina;
            return maximo < 1 ? string.Empty : $" OFFSET {(pagina - 1) * maximo} ROWS FETCH FIRST {maximo}  ROWS ONLY ";
        }

        /// <summary>
        /// Quebra um texto em várias partes para aumentar o alcance da busca 
        /// </summary>
        public IList<string> DesmebrarTexto(string tratar)
        {
            List<string> resultado = new List<string>();

            if (string.IsNullOrWhiteSpace(tratar))
                return resultado;

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

            return resultado.Where(x => x.Length > 2).Distinct().ToList();
        }
    }
}
