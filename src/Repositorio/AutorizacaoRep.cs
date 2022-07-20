using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.AutorizacaoPers;

namespace TemplateApi.Repositorio
{
    internal class AutorizacaoRep
        : Comum.BaseRep, IAutorizacaoRep
    {
        public AutorizacaoRep(
            ListarAutorizacaoPers persListarAutorizacao)
        {
            _persListarAutorizacao = persListarAutorizacao;
        }

        private readonly ListarAutorizacaoPers _persListarAutorizacao;

        public Autorizacao[] Listar()
        {
            Notifications.Clear();

            Autorizacao[] resultado = _persListarAutorizacao.Listar();
            Validate(_persListarAutorizacao);

            return resultado;
        }
    }
}
