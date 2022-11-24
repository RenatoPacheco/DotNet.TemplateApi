using System;
using System.Reflection;
using TemplateApi.Compartilhado;
using TemplateApi.Dominio.Interfaces.Repositorios;

namespace TemplateApi.Infra.Auxiliares
{
    public class ModuloDependencias
        : IModuloDependencias
    {
        public Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public Type[] Singleton => Array.Empty<Type>();

        public Type[] Scoped => Array.Empty<Type>();

        public string[] StarClasstNamespace => new string[]
        {
            GetStartNamespace(typeof(Recursos.Core.Servicos.SobreServ.ObterSobreServ)),
            GetStartNamespace(typeof(Recursos.Banco.TemplateApi.Servicos.ConteudoServ.FiltrarConteudoServ))
        };

        public string[] ExactClassNamespace => Array.Empty<string>();

        public string[] StartInterfaceNamespace => Array.Empty<string>();

        public string[] ExactInterfaceNamespace => new string[]
        {
            typeof(ISobreRep).Namespace
        };

        public void Registrar(IResolverDependencias resolve) 
        {
            resolve.Escopo<Recursos.Banco.TemplateApi.Conexao>();
        }

        private string GetStartNamespace(Type type)
        {
            return type.Namespace.Substring(
                0, type.Namespace.LastIndexOf('.'));
        }
    }
}