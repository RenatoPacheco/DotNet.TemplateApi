using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Repositorio
{
    internal class SobreRep
        : Comum.BaseRep, ISobreRep
    {
        public Sobre Obter()
        {
            Notifications.Clear();
            return new Sobre();
        }
    }
}
