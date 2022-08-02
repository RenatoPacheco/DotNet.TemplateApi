using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Servico.Core.Servicos.SobreServ;

namespace TemplateApi.Repositorio.Persistencias.SobrePers
{
    internal class ObterSobrePers
        : Comum.BaseRepositorio
    {
        public ObterSobrePers(
            ObterSobreServ servObterSobre)
        {
            _servObterSobre = servObterSobre;
        }

        private readonly ObterSobreServ _servObterSobre;

        public Sobre Executar()
        {
            Notifications.Clear();
            
            Sobre resultado = _servObterSobre.Executar();
            IsValid(_servObterSobre);
            
            return resultado;
        }
    }
}
