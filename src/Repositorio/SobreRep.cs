using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.SobrePers;

namespace TemplateApi.Repositorio
{
    internal class SobreRep
        : Comum.BaseRepositorio, ISobreRep
    {
        public SobreRep(
            ObterSobrePers persObterSobre)
        {
            _persObterSobre = persObterSobre;
        }

        private readonly ObterSobrePers _persObterSobre;

        public Sobre Obter()
        {
            Notifications.Clear();

            Sobre resultado = _persObterSobre.Obter();
            Validate(_persObterSobre);


            return resultado;
        }
    }
}
