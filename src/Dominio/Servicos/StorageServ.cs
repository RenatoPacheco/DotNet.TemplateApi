using System.Linq;
using BitHelp.Core.Validation;
using System.Collections.Generic;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;
using DotNetCore.API.Template.Compartilhado.ObjetosDeValor;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Dominio.Interfaces.Repositorios;

namespace DotNetCore.API.Template.Dominio.Servicos
{
    public class StorageServ : Comum.BaseServ
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

            if (Validate(comando))
            {
                resultado = (Storage)_repStorage.Filtrar(new FiltrarStorageCmd { 
                    Alias = new List<string>() { comando.Alias },
                    Status = comando.Status,
                    Maximo = 1, Pagina = 1
                });
                Validate(_repStorage);
            }

            return resultado;
        }

        public ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando)
        {
            Notifications.Clear();
            ResultadoBusca<Storage> resultado = new ResultadoBusca<Storage>();

            if (Validate(comando))
            {
                resultado = _repStorage.Filtrar(comando);
                Validate(_repStorage);
            }

            return resultado;
        }

        public Storage[] Inserir(InserirStorageCmd comando)
        {
            Notifications.Clear();
            IList<Storage> resultado = new List<Storage>();
            Storage storage = null;

            if (Validate(comando))
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

            if (Validate(comando))
            {
                resultado = (Storage)_repStorage.Filtrar(new FiltrarStorageCmd
                {
                    Storage = new LongInput[] { comando.Storage.Value },
                    Maximo = 1,
                    Pagina = 1
                }, nameof(comando.Storage), ValidationType.Error);
                Validate(_repStorage);

                if (IsValid())
                {
                    comando.Aplicar(ref resultado);
                    _repStorage.Editar(resultado);
                    Validate(_repStorage);
                }
            }

            if (!IsValid())
                comando.Desfazer(ref resultado);

            return resultado;
        }

        public void Excluir(ExcluirStorageCmd comando)
        {
            Notifications.Clear();

            if (Validate(comando))
            {
                _repStorage.Excluir(comando);
                Validate(_repStorage);
            }
        }
    }
}
