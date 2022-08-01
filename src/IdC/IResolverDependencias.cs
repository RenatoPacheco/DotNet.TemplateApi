using System;

namespace TemplateApi.IdC
{
    public interface IResolverDependencias
    {
        #region unico

        void Unico<TConcrete>()
            where TConcrete : class;

        void Unico(Type objeto);

        void Unico(Type servico, Type objeto);

        void Unico<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico;

        #endregion

        #region Escopo

        void Escopo<TConcrete>()
            where TConcrete : class;

        void Escopo(Type objeto);

        void Escopo(Type servico, Type objeto);

        void Escopo<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico;

        #endregion

        #region Transiente

        void Transiente<TConcrete>()
            where TConcrete : class;

        void Transiente(Type objeto);

        void Transiente(Type servico, Type objeto);

        void Transiente<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico;

        #endregion
    }
}
