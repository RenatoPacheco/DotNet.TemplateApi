using System;
using System.Reflection;
using TemplateApi.Compartilhado;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.SobrePers;

namespace TemplateApi.Repositorio.Auxiliares
{
    public class ModuloDependencias
        : IModuloDependencias
    {
        public Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public Type[] Singleton => Array.Empty<Type>();

        public Type[] Scoped => Array.Empty<Type>();

        public string[] StarClasstNamespace => new string[]
        {
            GetStartNamespace(typeof(ObterSobrePers))
        };

        public string[] ExactClassNamespace => new string[]
        {
            typeof(SobreRep).Namespace
        };

        public string[] StartInterfaceNamespace => Array.Empty<string>();

        public string[] ExactInterfaceNamespace => new string[]
        {
            typeof(ISobreRep).Namespace
        };

        public void Registrar(IResolverDependencias resolve) { }

        private string GetStartNamespace(Type type)
        {
            return type.Namespace.Substring(
                0, type.Namespace.LastIndexOf('.'));
        }
    }
}