using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Comandos.ConteudoCmds;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Interfaces.Repositorios
{
    public interface IConteudoRep : ISelfValidation
    {
        void Inserir(Conteudo dados);

        void Editar(Conteudo dados);

        void Excluir(ExcluirConteudoCmd comando);


        ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando, string referencia);

        ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando, ValidationType tipo);

        ResultadoBusca<Conteudo> Filtrar(FiltrarConteudoCmd comando, string referencia = "", ValidationType tipo = ValidationType.Alert);
    }
}
