namespace TemplateApi.Infra.Servico.Banco.TemplateApi
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
