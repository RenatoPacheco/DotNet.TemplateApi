namespace TemplateApi.Infra.Banco.TemplateApi
{
    internal class BaseBuscaServico
        : Comum.BaseBuscaMsSqlRepositorio
    {
        public BaseBuscaServico(
            Conexao conexao)
        {
            Conexao = conexao;
        }

        protected readonly Conexao Conexao;
    }
}
