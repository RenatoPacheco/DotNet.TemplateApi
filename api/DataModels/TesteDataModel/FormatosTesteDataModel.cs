using BitHelp.Core.Type.pt_BR;
using TemplateApi.Dominio.ObjetosDeValor;
using TemplateApi.Compartilhado.ObjetosDeValor;

namespace TemplateApi.Api.DataModels.TesteDataModel
{
    public class FormatosTesteDataModel
        : Common.BaseDataModel<FormatosTesteDataModel>
    {
        private string _string;
        public string String
        {
            get => _string;
            set
            {
                _string = value;
                RegistarPropriedade();
            }
        }

        private IntInput _int;
        public IntInput Int
        {
            get => _int;
            set
            {
                _int = value;
                RegistarPropriedade();
            }
        }

        private LongInput _long;
        public LongInput Long
        {
            get => _long;
            set
            {
                _long = value;
                RegistarPropriedade();
            }
        }

        private DecimalInput _decimal;
        public DecimalInput Decimal
        {
            get => _decimal;
            set
            {
                _decimal = value;
                RegistarPropriedade();
            }
        }

        private DoubleInput _double;
        public DoubleInput Double
        {
            get => _double;
            set
            {
                _double = value;
                RegistarPropriedade();
            }
        }

        private FloatInput _float;
        public FloatInput Float
        {
            get => _float;
            set
            {
                _float = value;
                RegistarPropriedade();
            }
        }

        private BoolInput _bool;
        public BoolInput Bool
        {
            get => _bool;
            set
            {
                _bool = value;
                RegistarPropriedade();
            }
        }

        private DateTimeInput _dateTime;
        public DateTimeInput DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                RegistarPropriedade();
            }
        }

        private TimeSpanInput _timeSpan;
        public TimeSpanInput TimeSpan
        {
            get => _timeSpan;
            set
            {
                _timeSpan = value;
                RegistarPropriedade();
            }
        }

        private GuidInput _guid;
        public GuidInput Guid
        {
            get => _guid;
            set
            {
                _guid = value;
                RegistarPropriedade();
            }
        }

        private EnumInput<Status> _enum;
        public EnumInput<Status> Enum
        {
            get => _enum;
            set
            {
                _enum = value;
                RegistarPropriedade();
            }
        }

        private PhoneType? _phone;
        public PhoneType? Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                RegistarPropriedade();
            }
        }
    }
}
