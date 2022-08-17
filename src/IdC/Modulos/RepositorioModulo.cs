using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Repositorio;
using TemplateApi.Dominio.Interfaces.Repositorios;

namespace TemplateApi.IdC.Modulos
{
    internal static class RepositorioModulo
    {
        private static string GetStartNamespace(Type type)
        {
            return type.Namespace.Substring(
                0, type.Namespace.LastIndexOf('.'));
        }

        internal static void Carregar(IResolverDependencias resolve)
        {
            Type baseType = typeof(SobreRep);
            string[] exactNamespace = new string[]
            {
                typeof(SobreRep).Namespace
            };
            string[] startNamespace = new string[]
            {
                GetStartNamespace(typeof(Repositorio.Persistencias.SobrePers.ObterSobrePers)),
            };
            string[] interfaceNamespace = new string[]
            {
                typeof(ISobreRep).Namespace
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
            Type _class, _interface;

            for (int count = 0; count < total; count++)
            {
                _class = listType[count];
                _interface = _class.GetInterfaces().Where(
                    x => interfaceNamespace.Any(y => y == x.Namespace)).FirstOrDefault();

                if (_interface is null)
                {
                    resolve.Escopo(_class);
                }
                else
                {
                    resolve.Escopo(_interface, _class);
                }
            }
        }
    }
}

