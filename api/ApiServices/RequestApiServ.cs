using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TemplateApi.Api.ApiServices
{
    public class RequestApiServ : Common.BaseApiServices
    {
        public RequestApiServ(
               IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;
        }

        protected readonly IHttpContextAccessor _httpAccessor;

        private bool GetKey(string[] source, string compare, out string output)
        {
            output = source.Where(x => string.Equals(x, compare,
                   StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            return !string.IsNullOrWhiteSpace(output);
        }

        public string[] GetHeader(string key)
        {
            HttpRequest request = _httpAccessor.HttpContext.Request;
            IHeaderDictionary headers = request.Headers;

            return GetKey(headers.Keys.ToArray(), key, out string output)
                ? headers[output].ToArray() : Array.Empty<string>();
        }

        public string[] GetQuery(string key)
        {
            HttpRequest request = _httpAccessor.HttpContext.Request;
            IQueryCollection query = request.Query;

            return GetKey(query.Keys.ToArray(), key, out string output)
                ? query[output].ToArray() : Array.Empty<string>();
        }

        public string GetBaseUrl()
        {
            HttpRequest request = _httpAccessor.HttpContext.Request;
            if (request == null) return null;
            UriBuilder uriBuilder = new UriBuilder(request.Scheme, request.Host.Host, request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }

            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}
