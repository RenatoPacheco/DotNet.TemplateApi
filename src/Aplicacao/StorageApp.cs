using System;
using System.Reflection;
using DotNetCore.API.Template.Dominio.Servicos;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Dominio.Notacoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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

        /// <summary>
        /// Permite obter um arquivo do storage apartir do seu alias. 
        /// </summary>
        [AcessoBasico]
        [Display(Name = "Obter arquivo do storage")]
        [Description("Permite obter um arquivo do storage apartir do seu alias.")]
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

        /// <summary>
        /// Permite filtrar os arquivos disponíveis no storage.
        /// </summary>
        [Display(Name = "Filtrar arquivos do storage")]
        [Description("Permite filtrar os arquivos disponíveis no storage.")]
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

        /// <summary>
        /// Permite inserir um ou mais arquivos ao storage.
        /// </summary>
        [Display(Name = "Inserir arquivo do storage")]
        [Description("Permite inserir um ou mais arquivos ao storage.")]
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

        /// <summary>
        /// Permite editar alguns dados de um storage específico.
        /// </summary>
        [Display(Name = "Editar arquivo do storage")]
        [Description("Permite editar alguns dados de um storage específico.")]
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

        /// <summary>
        /// Permite excluir um ou mais arquivos do storage.
        /// </summary>
        [Display(Name = "Excluir arquivo do storage")]
        [Description("Permite excluir um ou mais arquivos do storage.")]
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
