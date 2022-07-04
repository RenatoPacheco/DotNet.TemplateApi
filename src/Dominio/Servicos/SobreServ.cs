using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class SobreServ : Comum.BaseServ
    {
        public SobreServ(
            ISobreRep repSobre)
        {
            _repSobre = repSobre;
        }

        protected readonly ISobreRep _repSobre;

        public Sobre Obter()
        {
            Notifications.Clear();
            Sobre resultado = _repSobre.Obter();
            Validate(_repSobre);

            return resultado;
        }
    }
}
