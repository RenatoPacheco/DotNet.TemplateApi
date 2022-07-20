using System;
using System.Net;
using System.Linq;
using System.Reflection;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.Helpers;
using TemplateApi.Api.ViewsData;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Dominio.ObjetosDeValor;
using Microsoft.AspNetCore.Hosting;
using TemplateApi.RecursoResx;

namespace TemplateApi.Api.Controllers.Common
{
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(ComumViewsData))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "", typeof(ComumViewsData))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "", typeof(ComumViewsData))]
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
                    return MontarResultado.Json(
                        HttpStatusCode.Unauthorized, Notifications, data);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return MontarResultado.Json(
                    HttpStatusCode.BadRequest, Notifications, data);
            }

            Response.StatusCode = (int)HttpStatusCode.OK;
            return MontarResultado.Json(
                HttpStatusCode.OK, Notifications, data);
        }

        protected IActionResult CustomPhysicalFile(Storage data, IWebHostEnvironment webHostingEnvironment, bool download = false)
        {
            if (!IsValid())
            {
                if (Notifications.Messages.Any(x => x.Type == ValidationType.Unauthorized))
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return MontarResultado.Json(
                        HttpStatusCode.Unauthorized, Notifications);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return MontarResultado.Json(
                    HttpStatusCode.BadRequest, Notifications);
            }

            string contentRootPath = webHostingEnvironment.ContentRootPath;
            string finalPath = $"{contentRootPath}\\{data.Referencia}";

            if (System.IO.File.Exists(finalPath))
            {
                if (!string.IsNullOrWhiteSpace(data?.Checksum))
                    Response.Headers.Add("Checksum", data.Checksum);

                if (download)
                    return PhysicalFile(finalPath, data.Tipo, data.Alias);
                else
                    return PhysicalFile(finalPath, data.Tipo);
            }

            Notifications.AddError(string.Format(AvisosResx.XNaoEncontrado, NomesResx.Storage));
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return MontarResultado.Json(
                HttpStatusCode.NotFound, Notifications);
        }

        protected IActionResult CustomFile(byte[] bytes, string type, string name, bool download = false)
        {
            if (!IsValid())
            {
                if (Notifications.Messages.Any(x => x.Type == ValidationType.Unauthorized))
                {
                    Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return MontarResultado.Json(
                        HttpStatusCode.Unauthorized, Notifications);
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return MontarResultado.Json(
                    HttpStatusCode.BadRequest, Notifications);
            }

            if (download)
                return File(bytes, type, name);
            else
                return File(bytes, type);
        }
    }
}
