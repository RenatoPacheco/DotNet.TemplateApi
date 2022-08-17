using BitHelp.Core.Type.pt_BR;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.TesteDataModel
{
    public class FormatosTesteDataModel
    {
        public string String { get; set; }

        public IntInput Int { get; set; }

        public LongInput Long { get; set; }

        public DecimalInput Decimal { get; set; }

        public DoubleInput Double { get; set; }

        public BoolInput Bool { get; set; }

        public DateTimeInput DateTime { get; set; }

        public TimeSpanInput TimeSpan { get; set; }

        public GuidInput Guid { get; set; }

        public EnumInput<Status> Enum { get; set; }

        public PhoneType? Phone { get; set; }
    }
}
