using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;

namespace TemplateApi.Api.Controllers.Common
{
    public class BaseMvcController : Controller
    {
        protected ValidationNotification Notifications { get; set; } = new ValidationNotification();

        protected bool Validate(ISelfValidation valor)
        {
            Notifications.Add(valor);
            return valor.IsValid();
        }
    }
}
