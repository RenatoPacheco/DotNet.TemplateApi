using System;
using System.Reflection;
using TemplateApi.Dominio.Servicos;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.Notacoes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TemplateApi.Aplicacao.Intreceptadores;

namespace TemplateApi.Aplicacao
{
    public class StorageApp : Comum.BaseAplicacao
    {
        public StorageApp(
            AutenticacaoServ servAutenticacao,
            StorageServ servStorage,
            StorageInter interStorage)
            : base(servAutenticacao)
        {
            _servStorage = servStorage;
            _interStorage = interStorage;
        }

        protected readonly StorageServ _servStorage;
        protected readonly StorageInter _interStorage;

        /// <summary>
        /// Permite obter um arquivo do storage apartir do seu alias. 
        /// </summary>
        [NaoRequerAutorizacao]
        [Display(Name = "Obter arquivo do storage")]
        [Description("Permite obter um arquivo do storage apartir do seu alias.")]
        public Storage Obter(ObterStorageCmd comando)
        {
            Notifications.Clear();
            Storage resultado = null;

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interStorage.Obter(comando);
                resultado = _servStorage.Obter(comando);
                IsValid(_servStorage);
            }

            return resultado;
        }

        /// <summary>
        /// Permite filtrar os arquivos disponíveis no storage.
        /// </summary>
        [NaoRequerAutorizacao]
        [Display(Name = "Filtrar arquivos do storage")]
        [Description("Permite filtrar os arquivos disponíveis no storage.")]
        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interStorage.Filtrar(comando);
                resultado = _servStorage.Filtrar(comando);
                IsValid(_servStorage);
            }

            return resultado;
        }

        /// <summary>
        /// Permite inserir um ou mais arquivos ao storage.
        /// </summary>
        [Display(Name = "Inserir arquivo do storage")]
        [Description("Permite inserir um ou mais arquivos ao storage.")]
        public ResultadoBusca<Storage> Inserir(InserirStorageCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();

            if (EhAutorizado(MethodBase.GetCurrentMethod()))
            {
                _interStorage.Inserir(comando);
                resultado = _servStorage.Inserir(comando);
                IsValid(_servStorage);
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
                _interStorage.Editar(comando);
                resultado = _servStorage.Editar(comando);
                IsValid(_servStorage);
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
                _interStorage.Excluir(comando);
                _servStorage.Excluir(comando);
                IsValid(_servStorage);
            }
        }
    }
}
