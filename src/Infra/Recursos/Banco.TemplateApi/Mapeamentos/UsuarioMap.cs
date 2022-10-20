using System.Text;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Extensoes;

namespace TemplateApi.Infra.Recursos.Banco.TemplateApi.Mapeamentos
{
    internal class UsuarioMap
        : Auxiliares.BaseMapeamento<Usuario>
    {
        public UsuarioMap()
        {
            DefinnirTabela("Usuario");

            Associar(x => x.Id, "Codigo_Usuario");
            Associar(x => x.Nome, "Nome_Usuario");
            Associar(x => x.Email, "Email_Usuario");
            Associar(x => x.CriadoEm, "DataCriacao_Usuario");
            Associar(x => x.AlteradoEm, "DataAlteracao_Usuario");
            Associar(x => x.Status, "Status_Usuario");
            Associar(x => x.Telefone, "Telefone_Usuario");
            Associar(x => x.Senha, "Senha_Usuario");
        }

        public override string ToString()
        {
            StringBuilder resultado = new StringBuilder();

            if (NaoIgnorar(x => x.Id))
            {
                resultado.Append($"{SqlParaJson(x => x.Id)},");
            }

            if (NaoIgnorar(x => x.Nome))
            {
                resultado.Append($"{SqlParaJson(x => x.Nome)},");
            }

            if (NaoIgnorar(x => x.Email))
            {
                resultado.Append($"{SqlParaJson(x => x.Email)},");
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

            if (NaoIgnorar(x => x.Telefone))
            {
                resultado.Append($"{SqlParaJson(x => x.Telefone)},");
            }

            return resultado.ToString().Substring(0, resultado.ToString().Length - 1);
        }
    }
}