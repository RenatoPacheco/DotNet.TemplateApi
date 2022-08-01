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

        public static void Registrar<TServico, TObjeto>(IResolverDependencias recipiente)
            where TServico : class
            where TObjeto : class, TServico
        {
            if (Scoped.Contains(typeof(TServico)))
                recipiente.Escopo<TServico, TObjeto>();
            else if (Singleton.Contains(typeof(TServico)))
                recipiente.Unico<TServico, TObjeto>();
            else
                recipiente.Transiente<TServico, TObjeto>();
        }

        public static void Registrar<TConcrete>(IResolverDependencias recipiente)
            where TConcrete : class
        {
            if (Scoped.Contains(typeof(TConcrete)))
                recipiente.Escopo<TConcrete>();
            else if (Singleton.Contains(typeof(TConcrete)))
                recipiente.Unico<TConcrete>();
            else
                recipiente.Transiente<TConcrete>();
        }

        public static void Registrar(IResolverDependencias recipiente, Type objeto)
        {
            if (Scoped.Contains(objeto))
                recipiente.Escopo(objeto);
            else if (Singleton.Contains(objeto))
                recipiente.Unico(objeto);
            else
                recipiente.Transiente(objeto);
        }

        public static void Registrar(IResolverDependencias recipiente, Type servico, Type objeto)
        {
            if (Scoped.Contains(servico))
                recipiente.Escopo(servico, objeto);
            else if (Singleton.Contains(servico))
                recipiente.Unico(servico, objeto);
            else
                recipiente.Transiente(servico, objeto);
        }

        public static void Carregar(IResolverDependencias recipiente)
        {
            AplicacaoModulo.Carregar(recipiente);
            ServicoModulo.Carregar(recipiente);
            RepositorioModulo.Carregar(recipiente);
            InfraModulo.Carregar(recipiente);
        }
    }
}
