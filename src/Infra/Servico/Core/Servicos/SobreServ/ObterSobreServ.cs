using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Infra.Servico.Core.Servicos.SobreServ
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
