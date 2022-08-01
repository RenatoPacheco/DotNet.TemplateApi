using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.Infra.Servicos.SobreServ;

namespace TemplateApi.Repositorio
{
    internal class SobreRep
        : Comum.BaseRepositorio, ISobreRep
    {
        public SobreRep(
            ObterSobreServ persObterSobre)
        {
            _persObterSobre = persObterSobre;
        }

        private readonly ObterSobreServ _persObterSobre;

        public Sobre Obter()
        {
            Notifications.Clear();

            Sobre resultado = _persObterSobre.Executar();
            IsValid(_persObterSobre);


            return resultado;
        }
    }
}
