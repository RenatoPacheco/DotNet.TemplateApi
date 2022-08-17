using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Dominio.Servicos;

namespace TemplateApi.IdC.Modulos
{
    internal static class ServicoModulo
    {
        internal static Type[] _singleton = Array.Empty<Type>();

        internal static Type[] _scoped = new Type[]
        {
            typeof(AutenticacaoServ)
        };

        internal static void Carregar(IResolverDependencias resolve)
        {
            Type baseType = typeof(SobreServ);
            string[] exactNamespace = new string[]
            {
                typeof(SobreServ).Namespace
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
