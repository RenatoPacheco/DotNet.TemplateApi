using System;
using System.Reflection;
using TemplateApi.Compartilhado;
using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Dominio.Auxiliares
{
    public class ModuloDependencias
        : IModuloDependencias
    {
        public Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public Type[] Singleton => Array.Empty<Type>();

        public Type[] Scoped => new Type[]
        {
            typeof(AutenticacaoServ)
        };

        public string[] StarClasstNamespace => Array.Empty<string>();

        public string[] ExactClassNamespace => new string[]
        {
            typeof(SobreServ).Namespace
        };

        public string[] StartInterfaceNamespace => Array.Empty<string>();

        public string[] ExactInterfaceNamespace => Array.Empty<string>();

        public void Registrar(IResolverDependencias resolve) { }
    }
}