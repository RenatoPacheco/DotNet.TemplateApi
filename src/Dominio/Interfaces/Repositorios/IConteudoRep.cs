using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.Comandos.ConteudoCmds;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Interfaces.Repositorios
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
