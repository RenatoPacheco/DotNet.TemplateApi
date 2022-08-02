using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Infra.Servico.Banco.TemplateApi.Servicos.StorageServ;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class InserirStoragePers
        : Comum.BaseRepositorio
    {
        public InserirStoragePers(
            InserirStorageServ servInserirStorage)
        {
            _servInserirStorage = servInserirStorage;
        }

        private readonly InserirStorageServ _servInserirStorage;

        public void Executar(Storage dados)
        {
            Notifications.Clear();

            _servInserirStorage.Executar(dados);
            IsValid(_servInserirStorage);
        }
    }
}
