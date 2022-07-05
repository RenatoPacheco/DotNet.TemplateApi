using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Repositorio.Persistencias.SobrePers
{
    internal class ObterSobrePers : Comum.BaseRep
    {
        public Sobre Obter()
        {
            Notifications.Clear();
            return new Sobre();
        }
    }
}
