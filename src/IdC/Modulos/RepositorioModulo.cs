using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Repositorio;
using TemplateApi.Dominio.Interfaces.Repositorios;

namespace TemplateApi.IdC.Modulos
{
    internal static class RepositorioModulo
    {
        internal static Type[] _singleton = Array.Empty<Type>();

        internal static Type[] _scoped = new Type[]
        {
            typeof(AutorizacaoRep),
            typeof(Infra.Banco.TemplateApi.Conexao)
        };

        private static string GetStartNamespace(Type type)
        {
            return type.Namespace.Substring(
                0, type.Namespace.LastIndexOf('.'));
        }

        internal static void Carregar(IResolverDependencia recipiente)
        {
            Injecao.Registrar<Infra.Banco.TemplateApi.Conexao>(recipiente);

            Type baseType = typeof(SobreRep);
            string[] exactNamespace = new string[]
            {
                typeof(SobreRep).Namespace
            };
            string[] startNamespace = new string[]
            {
                GetStartNamespace(typeof(Repositorio.Persistencias.SobrePers.ObterSobrePers)),
                GetStartNamespace(typeof(Infra.Core.Servicos.SobreServ.ObterSobreServ)),
                GetStartNamespace(typeof(Infra.Banco.TemplateApi.Servicos.ConteudoServ.FiltrarConteudoServ))
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
                    Injecao.Registrar(recipiente, _class);
                }
                else
                {
                    Injecao.Registrar(recipiente, _interface, _class);
                }
            }
        }
    }
}

