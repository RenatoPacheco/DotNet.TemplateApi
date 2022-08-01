namespace TemplateApi.Infra.Banco.TemplateApi
{
    internal class BaseSimplesServico
        : Comum.BaseRepositorio
    {
        public BaseSimplesServico(
            Conexao conexao)
        {
            Conexao = conexao;
        }

        protected readonly Conexao Conexao;
    }
}
