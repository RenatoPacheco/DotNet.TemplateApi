using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Interfaces.Repositorios;

namespace TemplateApi.Dominio.Servicos
{
    public class SobreServ : Comum.BaseServico
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
