using DotNetCore.API.Template.Site.ValuesObject;

namespace DotNetCore.API.Template.Site.ViewsData
{
    public class ComumViewsData
    {
        public Avisos Avisos { get; set; }
    }

    public class ComumViewsData<T>
    {
        public Avisos Avisos { get; set; }

        public T Dados { get; set; }
    }
}