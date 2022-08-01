using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.Infra.Servicos.AutorizacaoServ;

namespace TemplateApi.Repositorio
{
    internal class AutorizacaoRep
        : Comum.BaseRepositorio, IAutorizacaoRep
    {
        public AutorizacaoRep(
            ListarAutorizacaoServ persListarAutorizacao)
        {
            _persListarAutorizacao = persListarAutorizacao;
        }

        private readonly ListarAutorizacaoServ _persListarAutorizacao;

        public Autorizacao[] Listar()
        {
            Notifications.Clear();

            Autorizacao[] resultado = _persListarAutorizacao.Executar();
            IsValid(_persListarAutorizacao);

            return resultado;
        }
    }
}
