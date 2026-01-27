using System.Reflection;
using TemplateApi.Compartilhados.IdC;
using TemplateApi.Aplicacoes.Interceptadores;

namespace TemplateApi.Aplicacoes.Auxiliares {
    public class ModuloDependencias
        : BaseModuloDependencias {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override string[] ExactClassNamespace => new string[]
        {
            typeof(SobreApp).Namespace,
            typeof(SobreInter).Namespace
        };
    }
}
