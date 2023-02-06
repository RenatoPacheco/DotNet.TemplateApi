using System;
using System.Linq;
using TemplateApi.Compartilhado.IdC;

namespace TemplateApi.IdC
{
    internal class ProcessarDependencias
    {
        private Type[] _singleton = Array.Empty<Type>();
        private Type[] Singleton
        {
            get => _singleton;
            set => _singleton = value ?? Array.Empty<Type>();
        }

        private Type[] _transient = Array.Empty<Type>();
        private Type[] Transient
        {
            get => _transient;
            set => _transient = value ?? Array.Empty<Type>();
        }

        public void Aplicar(IModuloDependencias modulo, IResolverDependencias resolve)
        {
            modulo.Registrar(resolve);

            Singleton = modulo.Singleton;
            Transient = modulo.Transient;
            string[] exactNamespace = modulo.ExactClassNamespace;
            string[] startNamespace = modulo.StarClasstNamespace;
            string[] exactInterface = modulo.ExactInterfaceNamespace;
            string[] startInterface = modulo.StartInterfaceNamespace;

            Type[] listType = modulo.Base
                .Where(x => x.ReflectedType is null
                        && !(x.Namespace is null)
                        && (exactNamespace.Contains(x.Namespace)
                            || startNamespace.Any(y => x.Namespace.StartsWith(y)))
                        && x.IsClass
                        && !x.IsAbstract
                        && !x.IsInterface
                        && !x.GetInterfaces().Any(
                            y => y == typeof(IModuloDependencias))).ToArray();

            int total = listType.Count();
            Type @class;
            Type[] interfaces;

            for (int count = 0; count < total; count++)
            {
                @class = listType[count];
                interfaces = @class.GetInterfaces().Where(
                    x => exactInterface.Contains(x.Namespace)
                        || startInterface.Any(y => x.Namespace.StartsWith(y))).Distinct().ToArray();

                if (interfaces.Any())
                {
                    foreach (Type @interface in interfaces)
                    {
                        Registrar(resolve, @interface, @class);
                    }
                }
                else
                {
                    Registrar(resolve, @class);
                }
            }
        }

        private void Registrar(IResolverDependencias resolve, Type objeto)
        {
            if (Transient.Contains(objeto))
                resolve.Transiente(objeto);
            else if (Singleton.Contains(objeto))
                resolve.Unico(objeto);
            else
                resolve.Escopo(objeto);
        }

        private void Registrar(IResolverDependencias resolve, Type servico, Type objeto)
        {
            if (Transient.Contains(servico))
                resolve.Transiente(servico, objeto);
            else if (Singleton.Contains(servico))
                resolve.Unico(servico, objeto);
            else
                resolve.Escopo(servico, objeto);
        }
    }
}
