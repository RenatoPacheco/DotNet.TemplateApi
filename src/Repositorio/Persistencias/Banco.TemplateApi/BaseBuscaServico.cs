namespace TemplateApi.Repositorio.Persistencias.Banco.TemplateApi
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
