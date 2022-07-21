using System.Text;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Mapeamentos
{
    internal class StorageMap
        : Auxiliares.MapeamentoBase<Storage>
    {
        public StorageMap()
        {
            DefinnirTabela("Storage");

            Associar(x => x.Id, "Codigo_Storage");
            Associar(x => x.Nome, "Nome_Storage");
            Associar(x => x.Alias, "Alias_Storage");
            Associar(x => x.Diretorio, "Diretorio_Storage");
            Associar(x => x.Extensao, "Extensao_Storage");
            Associar(x => x.Peso, "Peso_Storage");
            Associar(x => x.Tipo, "Tipo_Storage");
            Associar(x => x.Referencia, "Referencia_Storage");
            Associar(x => x.CriadoEm, "DataCriacao_Storage");
            Associar(x => x.AlteradoEm, "DataAlteracao_Storage");
            Associar(x => x.Checksum, "Checksum_Storage");
            Associar(x => x.Status, "Status_Storage");
        }

        public override string ToString()
        {
            StringBuilder resultado = new StringBuilder();

            if (NaoIgnorar(x => x.Id))
                resultado.Append($"{SqlParaJson(x => x.Id)},");

            if (NaoIgnorar(x => x.Nome))
                resultado.Append($"{SqlParaJson(x => x.Nome)},");

            if (NaoIgnorar(x => x.Alias))
                resultado.Append($"{SqlParaJson(x => x.Alias)},");

            if (NaoIgnorar(x => x.Diretorio))
                resultado.Append($"{SqlParaJson(x => x.Diretorio)},");

            if (NaoIgnorar(x => x.Extensao))
                resultado.Append($"{SqlParaJson(x => x.Extensao)},");

            if (NaoIgnorar(x => x.Tipo))
                resultado.Append($"{SqlParaJson(x => x.Tipo)},");

            if (NaoIgnorar(x => x.Checksum))
                resultado.Append($"{SqlParaJson(x => x.Checksum)},");

            if (NaoIgnorar(x => x.Peso))
                resultado.Append($"{SqlParaJson(x => x.Peso)},");

            if (NaoIgnorar(x => x.Referencia))
                resultado.Append($"{SqlParaJson(x => x.Referencia)},");

            if (NaoIgnorar(x => x.CriadoEm))
                resultado.Append($"{SqlParaJson(x => x.CriadoEm)},");

            if (NaoIgnorar(x => x.AlteradoEm))
                resultado.Append($"{SqlParaJson(x => x.AlteradoEm)},");

            if (NaoIgnorar(x => x.Status))
                resultado.Append($"{CharParaStatus(x => x.Status)},");

            return resultado.ToString().Substring(0, resultado.ToString().Length - 1);
        }
    }
}