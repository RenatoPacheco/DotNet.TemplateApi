using TemplateApi.Api.ValuesObject;
using TemplateApi.Dominio.ObjetosDeValor;

namespace TemplateApi.Api.ViewsData
{
    public class BuscaViewData<T>
    {
        public Avisos Avisos { get; set; }

        public ResultadoBusca<T> Dados { get; set; }
    }
}