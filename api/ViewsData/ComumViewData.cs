using TemplateApi.Api.ValuesObject;

namespace TemplateApi.Api.ViewsData
{
    public class ComumViewData
    {
        public Avisos Avisos { get; set; }
    }

    public class ComumViewsData<T>
    {
        public Avisos Avisos { get; set; }

        public T Dados { get; set; }
    }
}