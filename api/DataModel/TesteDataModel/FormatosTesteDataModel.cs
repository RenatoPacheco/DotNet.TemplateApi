using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModel.TesteDataModel
{
    public class FormatosTesteDataModel
    {
        public string String { get; set; }

        public IntInput? Int { get; set; }

        public LongInput? Long { get; set; }

        public DecimalInput? Decimal { get; set; }

        public DoubleInput? Double { get; set; }

        public DateTimeInput? DateTime { get; set; }

        public EnumInput<Status>? Enum { get; set; }
    }
}
