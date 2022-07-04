using System;
using System.Linq;
using System.Reflection;
using DotNetCore.API.Template.Repositorio;
using DotNetCore.API.Template.Dominio.Interfaces;
using DotNetCore.API.Template.Repositorio.Contexto;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.IdC.Modulos
{
    internal static class RepositorioModulo
    {
        internal static Type[] _singleton = Array.Empty<Type>();

        internal static Type[] _scoped = new Type[]
        {
            typeof(Conexao),
            typeof(ITransicao),
            typeof(IUnidadeTrabalho)
        };

        internal static void Carregar(IResolverDependencia recipiente)
        {
            Injecao.Registrar<Conexao>(recipiente);
            Injecao.Registrar<ITransicao, Transicao>(recipiente);
            Injecao.Registrar<IUnidadeTrabalho, UnidadeTrabalho>(recipiente);

            Type baseType = typeof(SobreRep);
            string[] exactNamespace = new string[]
            {
                typeof(SobreRep).Namespace
            };
            string[] startNamespace = new string[]
            {

            };
            string[] interfaceNamespace = new string[]
            {
                typeof(ISobreRep).Namespace
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

