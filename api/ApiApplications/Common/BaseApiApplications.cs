using BitHelp.Core.Validation;
namespace TemplateApi.Api.ApiApplications.Common
{
    public abstract class BaseApiApplications : ISelfValidation
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
