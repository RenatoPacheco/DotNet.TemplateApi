using System;
using System.Reflection;
using TemplateApi.Compartilhado;
using TemplateApi.Aplicacao.Intreceptadores;

namespace TemplateApi.Aplicacao.Auxiliares
{
    public class ModuloDependencias
        : IModuloDependencias
    {
        public Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public Type[] Singleton => Array.Empty<Type>();

        public Type[] Scoped => Array.Empty<Type>();

        public string[] StarClasstNamespace => Array.Empty<string>();

        public string[] ExactClassNamespace => new string[]
        {
            typeof(SobreApp).Namespace,
            typeof(SobreInter).Namespace
        };

        public string[] StartInterfaceNamespace => Array.Empty<string>();

        public string[] ExactInterfaceNamespace => Array.Empty<string>();

        public void Registrar(IResolverDependencias resolve) { }
    }
}