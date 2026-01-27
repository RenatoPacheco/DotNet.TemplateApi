using TemplateApi.Compartilhados.IdC;

namespace TemplateApi.IdC {
    public static class RegistrarDependencias {
        public static void Aplicar(IResolverDependencias resolve) {
            ProcessarDependencias processar = new();

            processar.Aplicar(new Aplicacoes.Auxiliares.ModuloDependencias(), resolve);
            processar.Aplicar(new Dominio.Auxiliares.ModuloDependencias(), resolve);
            processar.Aplicar(new Repositorios.Auxiliares.ModuloDependencias(), resolve);
            processar.Aplicar(new Infra.Auxiliares.ModuloDependencias(), resolve);
        }

        public static void Aplicar(IResolverDependencias resolve, IModuloDependencias modulo) {
            ProcessarDependencias processar = new();
            processar.Aplicar(modulo, resolve);
        }
    }
}
