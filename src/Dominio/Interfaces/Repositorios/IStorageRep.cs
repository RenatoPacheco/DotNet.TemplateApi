using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.Comandos.StorageCmds;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Interfaces.Repositorios
{
    public interface IStorageRep : ISelfValidation
    {
        void Inserir(Storage dados);

        void Editar(Storage dados);

        void Excluir(ExcluirStorageCmd comando);


        ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando, string referencia);

        ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando, ValidationType tipo);

        ResultadoBusca<Storage> Filtrar(FiltrarStorageCmd comando, string referencia = "", ValidationType tipo = ValidationType.Alert);
    }
}
