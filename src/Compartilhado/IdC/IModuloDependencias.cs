using System;

namespace TemplateApi.Compartilhado.IdC
{
    public interface IModuloDependencias
    {
        Type[] Base { get; }

        /// <summary>
        /// Indicar os tipos que devem ser registrados como Singleton
        /// </summary>
        Type[] Singleton { get; }

        /// <summary>
        /// Indicar os tipos que devem ser registrados como Transient
        /// </summary>
        Type[] Transient { get; }

        string[] StarClasstNamespace { get; }

        string[] ExactClassNamespace { get; }

        string[] StartInterfaceNamespace { get; }

        string[] ExactInterfaceNamespace { get; }

        /// <summary>
        /// Deve registrar individualmente uma classe quando necessário
        /// </summary>
        /// <param name="resolve">Informar a classe que vai resolver as dependencias</param>
        void Registrar(IResolverDependencias resolve);
    }
}
