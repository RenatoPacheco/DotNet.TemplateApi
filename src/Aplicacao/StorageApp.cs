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

        public Arquivo[] Inserir(InserirStorageCmd comando)
        {
            Notifications.Clear();
            Arquivo[] resultado = Array.Empty<Arquivo>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                resultado = _servStorage.Inserir(comando);
                Validate(_servStorage);
            }

            return resultado;
        }
    }
}
