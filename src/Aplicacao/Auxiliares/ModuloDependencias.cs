using System;
using System.Reflection;
using TemplateApi.Compartilhado.IdC;
using TemplateApi.Aplicacao.Intreceptadores;

namespace TemplateApi.Aplicacao.Auxiliares
{
    public class ModuloDependencias
        : BaseModuloDependencias
    {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override string[] ExactClassNamespace => new string[]
        {
            typeof(SobreApp).Namespace,
            typeof(SobreInter).Namespace
        };
    }
}