using System;
using System.Reflection;
using TemplateApi.Compartilhado;
using TemplateApi.Api.ApiServices;
using TemplateApi.Api.ApiApplications;

namespace TemplateApi.Api.Helpers
{
    public class ModuloDependencias
        : IModuloDependencias
    {
        public Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public Type[] Singleton => Array.Empty<Type>();

        public Type[] Scoped => new Type[] {
            typeof(AutorizacaoApiServ),
            typeof(RequestApiServ),
            typeof(AutenticacaoApiApp)
        };

        public string[] StarClasstNamespace => Array.Empty<string>();

        public string[] ExactClassNamespace => new string[]
        {
            typeof(AutorizacaoApiServ).Namespace,
            typeof(AutenticacaoApiApp).Namespace
        };

        public string[] StartInterfaceNamespace => Array.Empty<string>();

        public string[] ExactInterfaceNamespace => Array.Empty<string>();

        public void Registrar(IResolverDependencias resolve) { }
    }
}
