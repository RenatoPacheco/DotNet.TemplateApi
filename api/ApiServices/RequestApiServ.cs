using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DotNetCore.API.Template.Site.ApiServices
{
    public class RequestApiServ : Common.baseApiServ
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
    }
}
