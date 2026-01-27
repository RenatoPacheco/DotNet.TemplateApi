using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorios.Persistencias.AutorizacaoPers;

namespace TemplateApi.Repositorios
{
    internal class AutorizacaoRep
        : Comum.BaseRepositorio, IAutorizacaoRep
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

            Autorizacao[] resultado = _persListarAutorizacao.Executar();
            IsValid(_persListarAutorizacao);

            return resultado;
        }
    }
}
