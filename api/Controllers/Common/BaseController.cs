using System;
using System.Net;
using System.Linq;
using System.Reflection;
using TemplateApi.Recurso;
using BitHelp.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using TemplateApi.Api.ViewsData;
using TemplateApi.Api.Extensions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using TemplateApi.Dominio.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Api.ViewsData.CKEditorViewData;

namespace TemplateApi.Api.Controllers.Common
{
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "", typeof(ComumViewData))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "", typeof(ComumViewData))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "", typeof(ComumViewData))]
    public class BaseController : ControllerBase
    {
        protected ValidationNotification Notifications { get; set; } = new ValidationNotification();

        protected bool Validate(ISelfValidation valor)
        {
            Notifications.Add(valor);
            return valor.IsValid();
        }

        protected string Host => $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        protected string Path => $"{Host}{HttpContext.Request.Path}";

        protected void AplicarUrl(
            IArquivo arquivo, string path = null)
        {
            arquivo.Url = path is null ? Path : $"{Host}/{path}";
        }

        protected void AplicarUrl(
            IEnumerable<IArquivo> arquivos, string path = null)
        {
            int total = arquivos.Count();
            for (int i = 0; i < total; i++)
            {
                AplicarUrl(arquivos.ElementAt(i), path);
            }
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
                    return Response.ToJson(
                        HttpStatusCode.Unauthorized, Notifications, data);
                }

                return Response.ToJson(
                    HttpStatusCode.BadRequest, Notifications, data);
            }

            return Response.ToJson(
                HttpStatusCode.OK, Notifications, data);
        }

        protected IActionResult CKEditorV4Response(IArquivo data)
        {
            if (!IsValid())
            {
                return Response.ToJson(HttpStatusCode.OK, new V4CKEditorViewData
                {
                    Uploaded = 0,
                    FileName = string.Empty,
                    Url = string.Empty,
                    Error = new V4ErroCKEditorViewData
                    {
                        Message = string.Join("\n", Notifications.Messages.Select(x => x.Message))
                    }
                });
            }

            return Response.ToJson(HttpStatusCode.OK, new V4CKEditorViewData
            {
                Uploaded = 1,
                FileName = data?.Nome ?? string.Empty,
                Url = data?.Url ?? string.Empty,
                Error = new V4ErroCKEditorViewData
                {
                    Message = string.Empty
                }
            });
        }

        protected IActionResult CustomPhysicalFile(Storage data, IWebHostEnvironment webHostingEnvironment, bool? download = false)
        {
            download ??= false;

            if (!IsValid())
            {
                if (Notifications.Messages.Any(x => x.Type == ValidationType.Unauthorized))
                {
                    return Response.ToJson(
                        HttpStatusCode.Unauthorized, Notifications);
                }

                return Response.ToJson(
                    HttpStatusCode.BadRequest, Notifications);
            }

            string contentRootPath = webHostingEnvironment.ContentRootPath;
            string finalPath = $"{contentRootPath}\\{data.Referencia}";

            if (System.IO.File.Exists(finalPath))
            {
                if (!string.IsNullOrWhiteSpace(data?.Checksum))
                    Response.Headers.Add("Checksum", data.Checksum);

                if (download.Value)
                    return PhysicalFile(finalPath, data.Tipo, data.Alias);
                else
                    return PhysicalFile(finalPath, data.Tipo);
            }

            Notifications.AddError(string.Format(AvisosResx.XNaoEncontrado, NomesResx.Storage));

            return Response.ToJson(
                HttpStatusCode.NotFound, Notifications);
        }

        protected IActionResult CustomFile(byte[] bytes, string type, string name, bool? download = false)
        {
            download ??= false;

            if (!IsValid())
            {
                if (Notifications.Messages.Any(x => x.Type == ValidationType.Unauthorized))
                {
                    return Response.ToJson(
                        HttpStatusCode.Unauthorized, Notifications);
                }

                return Response.ToJson(
                    HttpStatusCode.BadRequest, Notifications);
            }

            if (download.Value)
                return File(bytes, type, name);
            else
                return File(bytes, type);
        }
    }
}
