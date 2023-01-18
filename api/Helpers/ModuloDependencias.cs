using System;
using System.Reflection;
using TemplateApi.Api.ApiServices;
using TemplateApi.Compartilhado.IdC;
using TemplateApi.Api.ApiApplications;
using TemplateApi.Api.App_Start.AutoMappers;

namespace TemplateApi.Api.Helpers
{
    public class ModuloDependencias
        : BaseModuloDependencias
    {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override Type[] Scoped => new Type[] {
            typeof(AutorizacaoApiServ),
            typeof(RequestApiServ),
            typeof(AutenticacaoApiApp)
        };

        public override string[] ExactClassNamespace => new string[]
        {
            typeof(AutorizacaoApiServ).Namespace,
            typeof(AutenticacaoApiApp).Namespace,
            typeof(ConteudoProfile).Namespace
        };
    }
}
