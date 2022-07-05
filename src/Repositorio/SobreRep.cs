using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;
using DotNetCore.API.Template.Repositorio.Persistencias.SobrePers;

namespace DotNetCore.API.Template.Repositorio
{
    internal class SobreRep
        : Comum.BaseRep, ISobreRep
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
