namespace TemplateApi.Repositorio.Persistencias.Banco.TemplateApi
{
    internal class Conexao : Contexto.ConexaoMsSql
    {
        protected override string ConnectionString => ConnectionStrings.TemplateApi;
    }
}
