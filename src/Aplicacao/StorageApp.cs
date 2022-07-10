using System;
using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;

namespace DotNetCore.API.Template.Aplicacao
{
    public class StorageApp : Comum.BaseApp
    {
        public StorageApp(
            StorageServ servStorage)
        {
            _servStorage = servStorage;
        }

        protected readonly StorageServ _servStorage;

        public Storage Obter(ObterStorageCmd comando)
        {
            Notifications.Clear();
            Storage resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servStorage.Obter(comando);
                Validate(_servStorage);
            }

            return resultado;
        }

        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servStorage.Filtrar(comando);
                Validate(_servStorage);
            }

            return resultado;
        }

        public Storage[] Inserir(InserirStorageCmd comando)
        {
            Notifications.Clear();
            Storage[] resultado = Array.Empty<Storage>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servStorage.Inserir(comando);
                Validate(_servStorage);
            }

            return resultado;
        }

        public Storage Editar(EditarStorageCmd comando)
        {
            Notifications.Clear();
            Storage resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servStorage.Editar(comando);
                Validate(_servStorage);
            }

            return resultado;
        }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _servStorage.Excluir(comando);
                Validate(_servStorage);
            }
        }
    }
}
