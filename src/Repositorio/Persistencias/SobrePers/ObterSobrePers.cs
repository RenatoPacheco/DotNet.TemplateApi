using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Persistencias.SobrePers
{
    internal class ObterSobrePers : Comum.BaseRepositorio
    {
        public Sobre Obter()
        {
            Notifications.Clear();
            return new Sobre();
        }
    }
}
