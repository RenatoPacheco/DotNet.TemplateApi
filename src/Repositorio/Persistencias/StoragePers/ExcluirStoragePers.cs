using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Recursos.Banco.TemplateApi.Servicos.StorageServ;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class ExcluirStoragePers
        : Comum.BaseRepositorio
    {
        public ExcluirStoragePers(
            ExcluirStorageServ servExcluirStorage)
        {
            _servExcluirStorage = servExcluirStorage;
        }

        private readonly ExcluirStorageServ _servExcluirStorage;

        public void Executar(ExcluirStorageCmd comando)
        {
            Notifications.Clear();

            _servExcluirStorage.Executar(comando);
            IsValid(_servExcluirStorage);
        }
    }
}
