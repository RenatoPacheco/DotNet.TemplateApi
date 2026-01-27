using TemplateApi.Api.ViewsData;
using TemplateApi.Dominio.Json;

namespace TemplateApi.Teste.Extensions {

    public static class HttpContentExt {

        public static async Task<T> ReadAsObjectAsync<T>(this HttpContent content) {
            string json = await content.ReadAsStringAsync();
            return ConverterJson.Desserializar<T>(json);
        }

        public static async Task<ComumViewData<T>> ReadAsViewDataAsync<T>(this HttpContent content) {
            return await content.ReadAsObjectAsync<ComumViewData<T>>();
        }
    }
}
