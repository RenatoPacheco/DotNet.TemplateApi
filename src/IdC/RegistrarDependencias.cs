using System;
using System.Linq;
using TemplateApi.IdC.Modulos;

namespace TemplateApi.IdC
{
    public static class RegistrarDependencias
    {
        private static Type[] _singleton;
        private static Type[] Singleton
        {
            get
            {
                return _singleton ?? (_singleton = AplicacaoModulo._singleton
                        .Concat(ServicoModulo._singleton).ToArray());
            }
        }

        private static Type[] _scoped;
        private static Type[] Scoped
        {
            get
            {
                return _scoped ?? (_scoped = AplicacaoModulo._scoped
                        .Concat(ServicoModulo._scoped).ToArray());
            }
        }

        public static void Registrar<TServico, TObjeto>(IResolverDependencias resolve)
            where TServico : class
            where TObjeto : class, TServico
        {
            if (Scoped.Contains(typeof(TServico)))
                resolve.Escopo<TServico, TObjeto>();
            else if (Singleton.Contains(typeof(TServico)))
                resolve.Unico<TServico, TObjeto>();
            else
                resolve.Transiente<TServico, TObjeto>();
        }

        public static void Registrar<TConcrete>(IResolverDependencias resolve)
            where TConcrete : class
        {
            if (Scoped.Contains(typeof(TConcrete)))
                resolve.Escopo<TConcrete>();
            else if (Singleton.Contains(typeof(TConcrete)))
                resolve.Unico<TConcrete>();
            else
                resolve.Transiente<TConcrete>();
        }

        public static void Registrar(IResolverDependencias resolve, Type objeto)
        {
            if (Scoped.Contains(objeto))
                resolve.Escopo(objeto);
            else if (Singleton.Contains(objeto))
                resolve.Unico(objeto);
            else
                resolve.Transiente(objeto);
        }

        public static void Registrar(IResolverDependencias resolve, Type servico, Type objeto)
        {
            if (Scoped.Contains(servico))
                resolve.Escopo(servico, objeto);
            else if (Singleton.Contains(servico))
                resolve.Unico(servico, objeto);
            else
                resolve.Transiente(servico, objeto);
        }

        public static void Carregar(IResolverDependencias resolve)
        {
            AplicacaoModulo.Carregar(resolve);
            ServicoModulo.Carregar(resolve);
            RepositorioModulo.Carregar(resolve);
            InfraModulo.Carregar(resolve);
        }
    }
}
