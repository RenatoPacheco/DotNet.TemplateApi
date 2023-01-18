using System;

namespace TemplateApi.Compartilhado.IdC
{
    public abstract class BaseModuloDependencias
        : IModuloDependencias
    {
        public abstract Type[] Base { get; }

        public virtual Type[] Singleton => Array.Empty<Type>();

        public virtual Type[] Transient => Array.Empty<Type>();

        public virtual string[] StarClasstNamespace => Array.Empty<string>();

        public virtual string[] ExactClassNamespace => Array.Empty<string>();

        public virtual string[] StartInterfaceNamespace => Array.Empty<string>();

        public virtual string[] ExactInterfaceNamespace => Array.Empty<string>();

        public virtual void Registrar(IResolverDependencias resolve) { }

        protected virtual string GetStartNamespace(Type type)
        {
            return type.Namespace.Substring(
                0, type.Namespace.LastIndexOf('.'));
        }
    }
}
