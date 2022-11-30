using System;
using System.Reflection;
using TemplateApi.Compartilhado.IdC;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.SobrePers;

namespace TemplateApi.Repositorio.Auxiliares
{
    public class ModuloDependencias
        : BaseModuloDependencias
    {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override string[] StarClasstNamespace => new string[]
        {
            GetStartNamespace(typeof(ObterSobrePers))
        };

        public override string[] ExactClassNamespace => new string[]
        {
            typeof(SobreRep).Namespace
        };

        public override string[] ExactInterfaceNamespace => new string[]
        {
            typeof(ISobreRep).Namespace
        };
    }
}