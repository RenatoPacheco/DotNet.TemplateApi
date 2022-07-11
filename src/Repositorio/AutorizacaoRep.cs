using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;
using DotNetCore.API.Template.Repositorio.Persistencias.AutorizacaoPers;

namespace DotNetCore.API.Template.Repositorio
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
