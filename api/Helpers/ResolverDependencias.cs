using System;
using TemplateApi.Compartilhado;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateApi.Api.Helpers
{
    public class ResolverDependencias 
        : IResolverDependencias
    {
        public ResolverDependencias(IServiceCollection service)
        {
            _service = service;
        }

        private  readonly IServiceCollection _service;

        #region unico

        public void Unico<TConcrete>()
            where TConcrete : class
        {
            _service.AddSingleton(typeof(TConcrete));
        }

        public void Unico(Type tipo)
        {
            _service.AddSingleton(tipo);
        }

        public void Unico<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico
        {
            _service.AddSingleton(typeof(TServico), typeof(TObjeto));
        }

        public void Unico(Type servico, Type objeto)
        {
            _service.AddSingleton(servico, objeto);
        }

        #endregion

        #region Escopo

        public void Escopo<TConcrete>()
            where TConcrete : class
        {
            _service.AddScoped(typeof(TConcrete));
        }

        public void Escopo(Type tipo)
        {
            _service.AddScoped(tipo);
        }

        public void Escopo<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico
        {
            _service.AddScoped(typeof(TServico), typeof(TObjeto));
        }

        public void Escopo(Type servico, Type objeto)
        {
            _service.AddScoped(servico, objeto);
        }

        #endregion

        #region Transiente

        public void Transiente<TConcrete>()
            where TConcrete : class
        {
            _service.AddTransient(typeof(TConcrete));
        }

        public void Transiente(Type tipo)
        {
            _service.AddTransient(tipo);
        }

        public void Transiente<TServico, TObjeto>()
            where TServico : class
            where TObjeto : class, TServico
        {
            _service.AddTransient(typeof(TServico), typeof(TObjeto));
        }

        public void Transiente(Type servico, Type objeto)
        {
            _service.AddTransient(servico, objeto);
        }

        #endregion
    }
}