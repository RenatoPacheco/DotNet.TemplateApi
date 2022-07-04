using System;
using System.Linq;
using System.Reflection;
using DotNetCore.API.Template.Aplicacao;
using DotNetCore.API.Template.Aplicacao.Intreceptadores;

namespace DotNetCore.API.Template.IdC.Modulos
{
    internal static class AplicacaoModulo
    {
        internal static Type[] _singleton = Array.Empty<Type>();

        internal static Type[] _scoped = Array.Empty<Type>();

        internal static void Carregar(IResolverDependencia recipiente)
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
                            || startNamespace.Any(y => y.StartsWith(x.Namespace)))
                        && x.IsClass
                        && !x.IsAbstract
                        && !x.IsInterface).ToArray();
            int total = listType.Count();

            for (int count = 0; count < total; count++)
            {
                Injecao.Registrar(recipiente, listType[count]);
            }
        }
    }
}