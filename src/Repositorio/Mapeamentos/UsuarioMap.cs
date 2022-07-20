using System.Text;
using TemplateApi.Dominio.Entidades;

namespace TemplateApi.Repositorio.Mapeamentos
{
    internal class UsuarioMap
        : Auxiliares.MapeamentoSqlBase<Usuario>
    {
        public UsuarioMap()
        {
            Associar(x => x.Id, "Codigo_Usuario");
            Associar(x => x.Nome, "Nome_Usuario");
            Associar(x => x.Email, "Email_Usuario");
            Associar(x => x.CriadoEm, "DataCriacao_Usuario");
            Associar(x => x.AlteradoEm, "DataAlteracao_Usuario");
            Associar(x => x.Status, "Status_Usuario");
            Associar(x => x.Telefone, "Telefone_Usuario");
            Associar(x => x.Senha, "Senha_Usuario");
        }

        public override string Tabela => "Usuario";

        public override string ToString()
        {
            StringBuilder resultado = new StringBuilder();

            if (NaoIgnorar(x => x.Id))
                resultado.Append($"{SqlParaJson(x => x.Id)},");

            if (NaoIgnorar(x => x.Nome))
                resultado.Append($"{SqlParaJson(x => x.Nome)},");

            if (NaoIgnorar(x => x.Email))
                resultado.Append($"{SqlParaJson(x => x.Email)},");

            if (NaoIgnorar(x => x.CriadoEm))
                resultado.Append($"{SqlParaJson(x => x.CriadoEm)},");

            if (NaoIgnorar(x => x.AlteradoEm))
                resultado.Append($"{SqlParaJson(x => x.AlteradoEm)},");

            if (NaoIgnorar(x => x.Status))
                resultado.Append($"{CharParaStatus(x => x.Status)},");

            if (NaoIgnorar(x => x.Telefone))
                resultado.Append($"{SqlParaJson(x => x.Telefone)},");

            return resultado.ToString().Substring(0, resultado.ToString().Length - 1);
        }
    }
}