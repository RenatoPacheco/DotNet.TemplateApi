using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Aplicacao;
using TemplateApi.Aplicacao.Intreceptadores;

namespace TemplateApi.IdC.Modulos
{
    internal static class AplicacaoModulo
    {
        internal static Type[] _singleton = Array.Empty<Type>();

        internal static Type[] _scoped = Array.Empty<Type>();

        internal static void Carregar(IResolverDependencias resolve)
        {
            Type baseType = typeof(SobreApp);
            string[] exactNamespace = new string[]
            {
                typeof(SobreApp).Namespace,
                typeof(SobreInter).Namespace
            };
            string[] startNamespace = new string[]
            {

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
                RegistrarDependencias.Registrar(resolve, listType[count]);
            }
        }
    }
}