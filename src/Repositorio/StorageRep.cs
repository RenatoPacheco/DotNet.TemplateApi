using System.Linq;
using BitHelp.Core.Validation;
using TemplateApi.Dominio.Interfaces;
using TemplateApi.Repositorio.Contexto;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Repositorio.Persistencias.StoragePers;

namespace TemplateApi.Repositorio
{
    internal class StorageRep
        : Comum.SimplesRep, IStorageRep
    {
        public StorageRep(
            Conexao conexao,
            IUnidadeTrabalho udt,
            InserirStoragePers persInserirStorage,
            EditarStoragePers persEditarStorage,
            ExcluirStoragePers persExcluirStorage,
            FiltrarStoragePers persFiltrarStorage)
            : base(conexao, udt) 
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

            _persEditarStorage.Editar(dados);
            Validate(_persEditarStorage);
        }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();

            _persExcluirStorage.Excluir(comando);
            Validate(_persExcluirStorage);
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

            ResultadoBusca<Storage> resultado = _persFiltrarStorage.Filtrar(comando, referencia, tipo);
            Validate(_persFiltrarStorage);

            return resultado;
        }

        public void Inserir(Storage dados)
        {
            Notifications.Clear();

            _persInserirStorage.Inserir(dados);
            Validate(_persInserirStorage);
        }
    }
}
