using BitHelp.Core.Validation;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Dominio.Interfaces.Repositorios
{
    public interface ISobreRep : ISelfValidation
    {
        Sobre Obter();
    }
}
