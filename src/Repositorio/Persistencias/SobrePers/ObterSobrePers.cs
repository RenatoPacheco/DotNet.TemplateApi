using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Persistencias.SobrePers
{
    internal class ObterSobrePers : Comum.BaseRepositorio
    {
        public Sobre Executar()
        {
            Notifications.Clear();
            return new Sobre();
        }
    }
}
