using System;
using DotNetCore.API.Template.IdC;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.API.Template.Site.Helpers
{
    public class ResolverDependencia : IResolverDependencia
    {
        public ResolverDependencia(IServiceCollection services)
        {
            _services = services;
        }

        private  readonly IServiceCollection _services;

        #region unico

        public void Unico<TConcrete>()
            where TConcrete : class
        {
            _services.AddSingleton(typeof(TConcrete));
        }

        public void Unico(Type tipo)
        {
            _services.AddSingleton(tipo);
        }

        public void Unico<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico
        {
            _services.AddSingleton(typeof(TServico), typeof(TObjeto));
        }

        public void Unico(Type servico, Type objeto)
        {
            _services.AddSingleton(servico, objeto);
        }

        #endregion

        #region Escopo

        public void Escopo<TConcrete>()
            where TConcrete : class
        {
            _services.AddScoped(typeof(TConcrete));
        }

        public void Escopo(Type tipo)
        {
            _services.AddScoped(tipo);
        }

        public void Escopo<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico
        {
            _services.AddScoped(typeof(TServico), typeof(TObjeto));
        }

        public void Escopo(Type servico, Type objeto)
        {
            _services.AddScoped(servico, objeto);
        }

        #endregion

        #region Transiente

        public void Transiente<TConcrete>()
            where TConcrete : class
        {
            _services.AddTransient(typeof(TConcrete));
        }

        public void Transiente(Type tipo)
        {
            _services.AddTransient(tipo);
        }

        public void Transiente<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico
        {
            _services.AddTransient(typeof(TServico), typeof(TObjeto));
        }

        public void Transiente(Type servico, Type objeto)
        {
            _services.AddTransient(servico, objeto);
        }

        #endregion
    }
}