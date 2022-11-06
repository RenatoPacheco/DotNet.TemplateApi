using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.StoragePers;

namespace TemplateApi.Repositorio
{
    internal class StorageRep
        : Comum.BaseRepositorio, IStorageRep
    {
        public StorageRep(
            InserirStoragePers persInserirStorage,
            EditarStoragePers persEditarStorage,
            ExcluirStoragePers persExcluirStorage,
            FiltrarStoragePers persFiltrarStorage)
            : base()
        {
            _persInserirStorage = persInserirStorage;
            _persEditarStorage = persEditarStorage;
            _persExcluirStorage = persExcluirStorage;
            _persFiltrarStorage = persFiltrarStorage;
        }

        private readonly InserirStoragePers _persInserirStorage;
        private readonly EditarStoragePers _persEditarStorage;
        private readonly ExcluirStoragePers _persExcluirStorage;
        private readonly FiltrarStoragePers _persFiltrarStorage;

        public void Editar(Storage dados)
        {
            Notifications.Clear();

            _persEditarStorage.Executar(dados);
            IsValid(_persEditarStorage);
        }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();

            _persExcluirStorage.Executar(comando);
            IsValid(_persExcluirStorage);
        }

        public ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, string referencia)
        {
            return Filtrar(comando, referencia, ValidationType.Alert);
        }

        public ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, ValidationType tipo)
        {
            return Filtrar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Storage> resultado = _persFiltrarStorage.Executar(comando, referencia, tipo);
            IsValid(_persFiltrarStorage);

            return resultado;
        }

        public void Inserir(Storage dados)
        {
            Notifications.Clear();

            _persInserirStorage.Executar(dados);
            IsValid(_persInserirStorage);
        }
    }
}
