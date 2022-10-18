using TemplateApi.Compartilhado;

namespace TemplateApi.IdC
{
    public static class RegistrarDependencias
    {
        public static void Aplicar(IResolverDependencias resolve)
        {
            ProcessarDependencias processar = new ProcessarDependencias();

            processar.Aplicar(new Aplicacao.Auxiliares.ModuloDependencias(), resolve);
            processar.Aplicar(new Dominio.Auxiliares.ModuloDependencias(), resolve);
            processar.Aplicar(new Repositorio.Auxiliares.ModuloDependencias(), resolve);
            processar.Aplicar(new Infra.Auxiliares.ModuloDependencias(), resolve);
        }
    }
}