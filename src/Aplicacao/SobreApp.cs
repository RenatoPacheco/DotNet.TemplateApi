using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Aplicacao
{
    public class SobreApp : Comum.BaseApp
    {
        public SobreApp(
            SobreServ servSobre)
        {
            _servSobre = servSobre;
        }

        protected readonly SobreServ _servSobre;

        public Sobre Obter()
        {
            Notifications.Clear();
            Sobre resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servSobre.Obter();
                Validate(_servSobre);
            }

            return resultado;
        }
    }
}
