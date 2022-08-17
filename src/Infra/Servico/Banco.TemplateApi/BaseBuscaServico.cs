namespace TemplateApi.Infra.Servico.Banco.TemplateApi
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
