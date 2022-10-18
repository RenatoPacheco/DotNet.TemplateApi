using System;

namespace TemplateApi.Compartilhado
{
    public interface IResolverDependencias
    {
        #region unico

        void Unico<TConcrete>()
            where TConcrete : class;

        void Unico(Type obteto);

        void Unico(Type servico, Type obteto);

        void Unico<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico;

        #endregion

        #region Escopo

        void Escopo<TConcrete>()
            where TConcrete : class;

        void Escopo(Type obteto);

        void Escopo(Type servico, Type obteto);

        void Escopo<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico;

        #endregion

        #region Transiente

        void Transiente<TConcrete>()
            where TConcrete : class;

        void Transiente(Type obteto);

        void Transiente(Type servico, Type obteto);

        void Transiente<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico;

        #endregion
    }
}