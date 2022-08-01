using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Aplicacao;

namespace TemplateApi.IdC.Modulos
{
    internal static class InfraModulo
    {
        private static string GetStartNamespace(Type type)
        {
            return type.Namespace.Substring(
                0, type.Namespace.LastIndexOf('.'));
        }

        internal static void Carregar(IResolverDependencias resolve)
        {
            resolve.Escopo<Infra.Banco.TemplateApi.Conexao>();

            Type baseType = typeof(SobreApp);
            string[] exactNamespace = new string[]
            {

            };
            string[] startNamespace = new string[]
            {
                GetStartNamespace(typeof(Infra.Core.Servicos.SobreServ.ObterSobreServ)),
                GetStartNamespace(typeof(Infra.Banco.TemplateApi.Servicos.ConteudoServ.FiltrarConteudoServ))
            };
            Type[] listType = Assembly.GetAssembly(baseType).GetTypes()
                    .Where(x => x.ReflectedType is null
                        && !(x.Namespace is null)
                        && (exactNamespace.Contains(x.Namespace)
                            || startNamespace.Any(y => x.Namespace.StartsWith(y)))
                        && x.IsClass
                        && !x.IsAbstract
                        && !x.IsInterface).ToArray();
            int total = listType.Count();

            for (int count = 0; count < total; count++)
            {
                resolve.Escopo(listType[count]);
            }
        }
    }
}