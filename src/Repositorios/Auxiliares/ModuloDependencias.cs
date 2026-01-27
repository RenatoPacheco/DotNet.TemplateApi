using System;
using System.Reflection;
using TemplateApi.Compartilhados.IdC;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorios.Persistencias.SobrePers;

namespace TemplateApi.Repositorios.Auxiliares
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