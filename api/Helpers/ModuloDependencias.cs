using System.Reflection;
using TemplateApi.Api.ApiServices;
using TemplateApi.Compartilhados.IdC;
using TemplateApi.Api.ApiApplications;

namespace TemplateApi.Api.Helpers {
    public class ModuloDependencias
        : BaseModuloDependencias {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override string[] ExactClassNamespace => new string[]
        {
            typeof(AutorizacaoApiServ).Namespace,
            typeof(AutenticacaoApiApp).Namespace
        };
    }
}
