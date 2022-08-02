namespace TemplateApi.Infra.Servico.Banco.TemplateApi
{
    internal class Conexao 
        : Contexto.ConexaoMsSql
    {
        protected override string ConnectionString => ConnectionStrings.TemplateApi;
    }
}
