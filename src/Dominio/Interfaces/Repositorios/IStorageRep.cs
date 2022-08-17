using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.StorageCmds;

namespace TemplateApi.Dominio.Interfaces.Repositorios
{
    public interface IStorageRep : ISelfValidation
    {
        void Inserir(Storage dados);

        void Editar(Storage dados);

        void Excluir(ExcluirStorageCmd comando);


        ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, string referencia);

        ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, ValidationType tipo);

        ResultadoBusca<Storage> Filtrar(
            FiltrarStorageCmd comando, string referencia = "", 
            ValidationType tipo = ValidationType.Alert);
    }
}
