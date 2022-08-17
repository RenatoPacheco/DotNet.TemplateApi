using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Servico.Core.Servicos.AutorizacaoServ;

namespace TemplateApi.Repositorio.Persistencias.AutorizacaoPers
{
    internal class ListarAutorizacaoPers
        : Comum.BaseRepositorio
    {
        public ListarAutorizacaoPers(
            ListarAutorizacaoServ servListarAutorizacao)
        {
            _servListarAutorizacao = servListarAutorizacao;
        }

        private readonly ListarAutorizacaoServ _servListarAutorizacao;

        public Autorizacao[] Executar()
        {
            Notifications.Clear();

            Autorizacao[] resultado = _servListarAutorizacao.Executar();
            IsValid(_servListarAutorizacao);

            return resultado;
        }
    }
}
