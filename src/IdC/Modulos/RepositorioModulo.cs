using System;
using System.Linq;
using System.Reflection;
using TemplateApi.Repositorio;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.SobrePers;
using TemplateApi.Repositorio.Interfaces;

namespace TemplateApi.IdC.Modulos
{
    internal static class RepositorioModulo
    {
        internal static Type[] _singleton = Array.Empty<Type>();

        internal static Type[] _scoped = new Type[]
        {
            typeof(IConexao),
            typeof(Conexao),
            typeof(ITransicao),
            typeof(IUnidadeTrabalho),
            typeof(AutorizacaoRep)
        };

        internal static void Carregar(IResolverDependencia recipiente)
        {
            Injecao.Registrar<IConexao, Conexao>(recipiente);
            Injecao.Registrar<Conexao>(recipiente);
            Injecao.Registrar<ITransicao, Transicao>(recipiente);
            Injecao.Registrar<IUnidadeTrabalho, UnidadeTrabalho>(recipiente);

            string repositorios = typeof(ObterSobrePers).Namespace;
            repositorios = repositorios.Substring(0, repositorios.LastIndexOf('.'));

            Type baseType = typeof(SobreRep);
            string[] exactNamespace = new string[]
            {
                typeof(SobreRep).Namespace
            };
            string[] startNamespace = new string[]
            {
                repositorios
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

