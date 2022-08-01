using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Infra.Core.Servicos.SobreServ
{
    internal class ObterSobreServ 
        : BaseServico
    {
        public Sobre Executar()
        {
            Notifications.Clear();
            return new Sobre();
        }
    }
}
