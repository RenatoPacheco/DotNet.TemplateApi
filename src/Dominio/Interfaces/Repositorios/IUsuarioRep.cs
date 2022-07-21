using BitHelp.Core.Validation;
using TemplateApi.Dominio.Entidades;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Dominio.Comandos.UsuarioCmds;

namespace TemplateApi.Dominio.Interfaces.Repositorios
{
    public interface IUsuarioRep : ISelfValidation
    {
        void Inserir(Usuario dados);

        void Editar(Usuario dados);

        void Excluir(ExcluirUsuarioCmd comando);


        ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, string referencia);

        ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, ValidationType tipo);

        ResultadoBusca<Usuario> Filtrar(
            FiltrarUsuarioCmd comando, string referencia = "", 
            ValidationType tipo = ValidationType.Alert);
    }
}
