using BitHelp.Core.Validation;
using DotNetCore.API.Template.Dominio.ObjetosDeValor;

namespace DotNetCore.API.Template.Dominio.Interfaces.Repositorios
{
    public interface IAutorizacaoRep : ISelfValidation
    {
        Autorizacao[] Listar();
    }
}
