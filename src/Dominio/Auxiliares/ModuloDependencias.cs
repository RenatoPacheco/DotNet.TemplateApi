using System;
using System.Reflection;
using TemplateApi.Compartilhado.IdC;
using TemplateApi.Dominio.Servicos;

namespace TemplateApi.Dominio.Auxiliares
{
    public class ModuloDependencias
        : BaseModuloDependencias
    {
        public override Type[] Base => Assembly.GetAssembly(typeof(ModuloDependencias)).GetTypes();

        public override Type[] Scoped => new Type[]
        {
            typeof(AutenticacaoServ)
        };

        public override string[] ExactClassNamespace => new string[]
        {
            typeof(SobreServ).Namespace
        };
    }
}