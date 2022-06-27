using BitHelp.Core.Validation;
using System.Reflection;

namespace DotNetCore.API.Template.Aplicacao.Comum
{
    public abstract class BaseApp : ISelfValidation
    {
        public ValidationNotification Notifications { get; protected set; } = new ValidationNotification();

        public bool Validate(ISelfValidation valor)
        {
            bool resultado = valor.IsValid();
            Notifications.Add(valor);
            return resultado;
        }

        public bool IsValid()
        {
            return Notifications.IsValid();
        }

        public bool EhAutorizado(MethodBase metodo)
        {
            return true;
        }
    }
}