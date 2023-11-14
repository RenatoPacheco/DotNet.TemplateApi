using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using TemplateApi.Api.ViewsData;
using System.Collections.Generic;
using TemplateApi.Compartilhado.Json;

namespace TemplateApi.Teste.Extensions
{
    public static class HttpContentExt
    {
        public static async Task<T> ReadAsObjectAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            return ContratoJson.Desserializar<T>(json);
        }

        public static async Task<ComumViewData<T>> ReadAsViewDataAsync<T>(this HttpContent content)
        {
            return await content.ReadAsObjectAsync<ComumViewData<T>>();
        }
    }
}
