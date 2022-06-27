using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.Entidades;
using DotNetCore.API.Template.Dominio.Comandos.UsuarioCmds;

namespace DotNetCore.API.Template.Dominio.Interfaces.Repositorios
{
    public interface IUsuarioRep : ISelfValidation
    {
        void Inserir(Usuario dados);

        void Editar(Usuario dados);

        void Excluir(ExcluirUsuarioCmd comando);

        Usuario[] Filtrar(FiltrarUsuarioCmd comando);
    }
}
