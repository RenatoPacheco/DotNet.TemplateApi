namespace TemplateApi.Infra.Recursos.Banco.TemplateApi
{
    internal class BaseBuscaServico
        : Comum.BaseBuscaMsSqlInfra
    {
        public BaseBuscaServico(
            Conexao conexao)
        {
            Conexao = conexao;
        }

        protected readonly Conexao Conexao;
    }
}
