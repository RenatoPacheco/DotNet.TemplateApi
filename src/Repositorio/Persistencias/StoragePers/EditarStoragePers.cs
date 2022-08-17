using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.StorageServ;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class EditarStoragePers
        : Comum.BaseRepositorio
    {
        public EditarStoragePers(
            EditarStorageServ servEditarStorage)
        {
            _servEditarStorage = servEditarStorage;
        }

        private readonly EditarStorageServ _servEditarStorage;

        public void Executar(Storage dados)
        {
            Notifications.Clear();

            _servEditarStorage.Executar(dados);
            IsValid(_servEditarStorage);
        }
    }
}
