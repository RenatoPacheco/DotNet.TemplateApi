using BitHelp.Core.Validation;

namespace TemplateApi.Api.ApiServices.Common
{
    public abstract class baseApiServ : ISelfValidation
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
    }
}
