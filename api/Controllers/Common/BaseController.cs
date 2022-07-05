using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Reflection;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.API.Template.Site.Helpers;

namespace DotNetCore.API.Template.Site.Controllers.Common
{
    public class BaseController : ControllerBase
    {
        protected ValidationNotification Notifications { get; set; } = new ValidationNotification();

        protected bool Validate(ISelfValidation valor)
        {
            Notifications.Add(valor);
            return valor.IsValid();
        }

        protected bool IsValid()
        {
            return Notifications.IsValid();
        }

        protected void InvocarSeNulo<TClasse>(ref TClasse classe)
            where TClasse : class
        {
            if (classe is null)
            {
                ConstructorInfo constructor = typeof(TClasse).GetConstructor(Type.EmptyTypes);
                classe = (TClasse)constructor.Invoke(null);
            }
        }

        protected IActionResult CustomResponse()
        {
            return CustomResponse(null);
        }

        protected IActionResult CustomResponse(object data)
        {
            if (!IsValid())
            {
                if (Notifications.Messages.Any(x => x.Type == ValidationType.Unauthorized))
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Content(MontarResultado.Json(
                        HttpStatusCode.Unauthorized, Notifications, data),
                        "application/json", Encoding.UTF8);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content(MontarResultado.Json(
                    HttpStatusCode.BadRequest, Notifications, data),
                        "application/json", Encoding.UTF8);
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Content(MontarResultado.Json(
                HttpStatusCode.OK, Notifications, data),
                    "application/json", Encoding.UTF8);
        }

        protected IActionResult CustomResponse(string data)
        {
            if (!IsValid())
            {
                if (Notifications.Messages.Any(x => x.Type == ValidationType.Unauthorized))
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Content(MontarResultado.Json(
                        HttpStatusCode.Unauthorized, Notifications, data),
                        "application/json", Encoding.UTF8);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content(MontarResultado.Json(
                    HttpStatusCode.BadRequest, Notifications, data),
                        "application/json", Encoding.UTF8);
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Content(MontarResultado.Json(
                HttpStatusCode.OK, Notifications, data),
                    "application/json", Encoding.UTF8);
        }
    }
}
