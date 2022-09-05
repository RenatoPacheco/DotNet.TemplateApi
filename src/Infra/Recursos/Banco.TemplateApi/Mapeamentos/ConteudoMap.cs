using System.Text;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Extensoes;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Mapeamentos
{
    internal class ConteudoMap
        : Auxiliares.BaseMapeamento<Conteudo>
    {
        public ConteudoMap()
        {
            DefinnirTabela("Conteudo");

            Associar(x => x.Id, "Codigo_Conteudo");
            Associar(x => x.Titulo, "Titulo_Conteudo");
            Associar(x => x.Texto, "Texto_Conteudo");
            Associar(x => x.Alias, "Alias_Conteudo");
            Associar(x => x.CriadoEm, "DataCriacao_Conteudo");
            Associar(x => x.AlteradoEm, "DataAlteracao_Conteudo");
            Associar(x => x.Status, "Status_Conteudo");
        }

        public override string ToString()
        {
            StringBuilder resultado = new StringBuilder();

            if (NaoIgnorar(x => x.Id))
            {
                resultado.Append($"{SqlParaJson(x => x.Id)},");
            }

            if (NaoIgnorar(x => x.Titulo))
            {
                resultado.Append($"{SqlParaJson(x => x.Titulo)},");
            }

            if (NaoIgnorar(x => x.Alias))
            {
                resultado.Append($"{SqlParaJson(x => x.Alias)},");
            }

            if (NaoIgnorar(x => x.Texto))
            {
                resultado.Append($"{SqlParaJson(x => x.Texto)},");
            }

            if (NaoIgnorar(x => x.CriadoEm))
            {
                resultado.Append($"{SqlParaJson(x => x.CriadoEm)},");
            }

            if (NaoIgnorar(x => x.AlteradoEm))
            {
                resultado.Append($"{SqlParaJson(x => x.AlteradoEm)},");
            }

            if (NaoIgnorar(x => x.Status))
            {
                resultado.Append($"{this.MsSqlCharParaEnum(x => x.Status, typeof(Status))},");
            }

            return resultado.ToString().Substring(0, resultado.ToString().Length - 1);
        }
    }
}