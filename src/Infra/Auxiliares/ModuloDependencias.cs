using System.Reflection;
using TemplateApi.Compartilhado.IdC;
using TemplateApi.Dominio.Interfaces.Repositorios;

namespace TemplateApi.Infra.Auxiliares
{
    public class ModuloDependencias
        : BaseModuloDependencias
    {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override string[] StarClasstNamespace => new string[]
        {
            GetStartNamespace(typeof(Recursos.Core.Servicos.SobreServ.ObterSobreServ)),
            GetStartNamespace(typeof(Recursos.Banco.TemplateApi.Servicos.ConteudoServ.FiltrarConteudoServ))
        };

        public override void Registrar(IResolverDependencias resolve)
        {
            resolve.Escopo<Recursos.Banco.TemplateApi.Conexao>();
        }
    }
}