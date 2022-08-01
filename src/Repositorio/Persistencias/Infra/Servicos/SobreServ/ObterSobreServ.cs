using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Repositorio.Persistencias.Infra.Servicos.SobreServ
{
    internal class ObterSobreServ : Comum.BaseRepositorio
    {
        public Sobre Executar()
        {
            Notifications.Clear();
            return new Sobre();
        }
    }
}
