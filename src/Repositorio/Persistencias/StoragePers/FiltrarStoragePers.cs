using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Infra.Banco.TemplateApi.Servicos.StorageServ;

namespace TemplateApi.Repositorio.Persistencias.StoragePers
{
    internal class FiltrarStoragePers
        : Comum.BaseRepositorio
    {
        public FiltrarStoragePers(
            FiltrarStorageServ servFiltrarStorage)
        {
            _servFiltrarStorage = servFiltrarStorage;
        }

        private readonly FiltrarStorageServ _servFiltrarStorage;

        public ResultadoBusca<Storage> Executar(
            FiltrarStorageCmd comando, string referencia)
        {
            return Executar(comando, referencia, ValidationType.Alert);
        }

        public ResultadoBusca<Storage> Executar(
            FiltrarStorageCmd comando, ValidationType tipo)
        {
            return Executar(comando, string.Empty, tipo);
        }

        public ResultadoBusca<Storage> Executar(
            FiltrarStorageCmd comando, string referencia = "",
            ValidationType tipo = ValidationType.Alert)
        {
            Notifications.Clear();

            ResultadoBusca<Storage> resultado = _servFiltrarStorage.Executar(comando, referencia, tipo);
            IsValid(_servFiltrarStorage);

            return resultado;
        }
    }
}
