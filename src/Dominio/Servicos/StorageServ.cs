using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;
using TemplateApi.Dominio.Interfaces.Repositorios;
using TemplateApi.Dominio.Comandos.Comum;

namespace TemplateApi.Dominio.Servicos
{
    public class StorageServ : Comum.BaseServico
    {
        public StorageServ(
            IStorageRep repStorage)
        {
            _repStorage = repStorage;
        }

        protected readonly IStorageRep _repStorage;

        public Storage Obter(ObterStorageCmd comando)
        {
            Notifications.Clear();
            Storage resultado = null;

            if (IsValid(comando))
            {
                resultado = (Storage)_repStorage.Filtrar(new FiltrarStorageCmd { 
                    Alias = new List<string>() { comando.Alias },
                    Status = comando.Status,
                    Maximo = 1, Pagina = 1
                }, string.Empty, ValidationType.Error);
                IsValid(_repStorage);
            }

            return resultado;
        }

        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();

            if (IsValid(comando))
            {
                resultado = _repStorage.Filtrar(comando);
                IsValid(_repStorage);
            }

            return resultado;
        }

        public Storage[] Inserir(InserirStorageCmd comando)
        {
            Notifications.Clear();
            IList<Storage> resultado = new List<Storage>();
            Storage storage = null;

            if (IsValid(comando))
            {
                foreach (Arquivo item in comando.Arquivo)
                {
                    comando.Aplicar(ref storage, item);
                    _repStorage.Inserir(storage);

                    if (_repStorage.IsValid())
                    {
                        resultado.Add(storage);
                    }
                    else
                    {
                        comando.Desfazer(ref storage, item);
                    }
                }
            }

            return resultado.ToArray();
        }



        public Storage Editar(EditarStorageCmd comando)
        {
            Notifications.Clear();
            Storage resultado = null;

            if (IsValid(comando))
            {
                resultado = (Storage)_repStorage.Filtrar(new FiltrarStorageCmd
                {
                    Storage = new long[] { comando.Storage.Value },
                    Contexto = ContextoCmd.Editar,
                    Maximo = 1,
                    Pagina = 1
                }, nameof(comando.Storage), ValidationType.Error);
                IsValid(_repStorage);

                if (IsValid())
                {
                    comando.Aplicar(ref resultado);
                    _repStorage.Editar(resultado);
                    IsValid(_repStorage);
                }
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();

            if (IsValid(comando))
            {
                _repStorage.Excluir(comando);
                IsValid(_repStorage);
            }
        }
    }
}
